using gen_fast_report.Attributes;
using gen_fast_report.Attributes.Balistica;
using gen_fast_report.Enums;
using gen_fast_report.Enums.Balistica;
using gen_fast_report.Models.Controllers;
using gen_fast_report.Models.DTOs;
using gen_fast_report.Services.IServices;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using Xceed.Document.NET;
using Xceed.Words.NET;

public class ManageBalisticaReportService  : IManageBalisticaReportService
{
    private readonly string _standardReportBalisticaPath;
    private readonly string _destinyPath;
    private readonly IManageReportService _manageReportService;
    private readonly ILogger<ManageBalisticaReportService> _logger;

    public ManageBalisticaReportService(IManageReportService manageReportService,
        IConfiguration configuration,
        ILogger<ManageBalisticaReportService> logger)
    {
        _manageReportService = manageReportService ?? throw new ArgumentNullException(nameof(manageReportService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _standardReportBalisticaPath = configuration["ReportPaths:StandardReportBalistica"]
                    ?? throw new InvalidOperationException("Caminho do relatório não configurado corretamente.");

        _destinyPath = configuration["ReportPaths:DestinyPath"]
            ?? throw new InvalidOperationException("Caminho de destino não configurado corretamente.");

    }

    public async Task<string> WriteNewReport(BalisticaRequest balisticaRequest)
    {
        try
        {
            IFormFile file = balisticaRequest.File!;
            string destinyPathComplete = Path.Combine(_destinyPath, file!.FileName);
            await using (var fileStream = new FileStream(destinyPathComplete, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            _logger.LogInformation("O documento foi copiado com sucesso !");

            // Get header values from input document
            BalisticaDTO balisticaReceive = _manageReportService.GetDataHeaderFromInputReport<BalisticaDTO>(destinyPathComplete);

            DocX document = DocX.Load(_standardReportBalisticaPath);

            if (document is not null && balisticaReceive is not null)
            {
                // Replace Header
                document = _manageReportService.ReplaceValuesFromHeader(document, balisticaReceive);

                // Replace Main Values
                document = ReplaceMainValues(document, balisticaRequest, balisticaReceive);

                // Save
                document.SaveAs(destinyPathComplete);
            }
            _logger.LogInformation("O documento foi gerado com sucesso !");
            return "Documento feito com sucesso";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao construir documento");
            throw;
        }
    }

    private DocX ReplaceMainValues(DocX document, BalisticaRequest balisticaRequest, BalisticaDTO balisticaReceiveDTO)
    {
        document = ReplaceGenderSpecificValues(document, balisticaRequest.Gender);
        document = ReplaceBalisticaTypeValues(document, balisticaRequest);
        document = ReplaceExamResults(document, balisticaRequest);
        document = ReplaceForwardingMaterial(document, balisticaRequest, balisticaReceiveDTO);
        return document;
    }

    private DocX ReplaceGenderSpecificValues(DocX document, Gender gender)
    {
        ReplaceGenderPlaceholder(document, gender, "#historico", "o Perito Criminal, signatário", "a Perita Criminal, signatária");
        return document;
    }

    private DocX ReplaceBalisticaTypeValues(DocX document, BalisticaRequest balisticaRequest)
    {
        switch (balisticaRequest.BalisticType)
        {
            case TipoBalistica.ArmaFogo:

                document.ReplaceText(new StringReplaceTextOptions
                {
                    SearchValue = "#objetivo1",
                    NewValue = " a arma de fogo encaminhada"
                });

                document.ReplaceText(new StringReplaceTextOptions
                {
                    SearchValue = "#objetivo2",
                    NewValue = " e número de série"
                });

                document = ReplaceMaterialDescriptionWeapon(document, balisticaRequest);

                break;
            case TipoBalistica.Municao:
                document.ReplaceText(new StringReplaceTextOptions
                {
                    SearchValue = "#objetivo1",
                    NewValue = " as munições encaminhadas"
                });
                document.ReplaceText(new StringReplaceTextOptions
                {
                    SearchValue = "#objetivo2",
                    NewValue = ""
                });
                break;
        }
        document = ReplaceInitialConsiderations(document, balisticaRequest);    
        return document;
    }

    private DocX ReplaceInitialConsiderations(DocX document, BalisticaRequest balisticaRequest)
    {
        ReplaceGenderPlaceholder(document, balisticaRequest.Gender, ReportPlaceholdersBalistica.Consideracao1, "o perito", "a perita");
        ReplaceAmountConsideration(document, balisticaRequest.Amount);
        return document;
    }

    private void ReplaceAmountConsideration(DocX document, int amount)
    {
        string amountText = amount == 1
            ? "na peça acima discriminada"
            : "nas peças acima discriminadas";

        Utils.ReplaceText(document, ReportPlaceholdersBalistica.Consideracao2, amountText);
    }

    private DocX ReplaceMaterialDescriptionWeapon(DocX document, BalisticaRequest balisticaRequest)
    {
        ReplaceEnvelopeNumber(document, balisticaRequest.EnvelopeNumber, ReportPlaceholdersBalistica.Involucro, ":");
        ReplaceWeaponDetails(document, balisticaRequest);
        ReplaceAdditionalDetails(document, balisticaRequest);
        return document;
    }

    private DocX ReplaceExamResults(DocX document, BalisticaRequest balisticaRequest)
    {
        switch (balisticaRequest.BallisticsExamResult)
        {
            case ResultadoExameBalistica.Eficiente:
                Utils.RemoveText(document, "#RESULTADO1");
                Utils.RemoveParagraphWithInitiateText(document, ["#RESULTADO2", "#RESULTADO3"]);
                break;
            case ResultadoExameBalistica.Ineficiente:
                Utils.RemoveText(document, "#RESULTADO2");
                Utils.RemoveParagraphWithInitiateText(document, ["#RESULTADO1", "#RESULTADO3"]);
                break;
            case ResultadoExameBalistica.Prejudicado:
                Utils.RemoveText(document, "#RESULTADO3");
                Utils.RemoveParagraphWithInitiateText(document, ["#RESULTADO1", "#RESULTADO2"]);
                break;
        }
        return document;
    }
    
    private DocX ReplaceForwardingMaterial(DocX document, BalisticaRequest balisticaRequest, BalisticaDTO balisticaReceiveDTO)
    {
        Utils.ReplaceText(document, "#ENCAMINHAMENTO1", balisticaReceiveDTO.Fav);
        ReplaceEnvelopeNumber(document, balisticaRequest.EnvelopeNumber, "#ENCAMINHAMENTO2");
        Utils.ReplaceText(document, "#ENCAMINHAMENTO3", Utils.GetEnumMemberValue(balisticaRequest.STRCS));
        return document;
    }
    private void ReplaceEnvelopeNumber(DocX document, int? envelopeNumber, string placeHolder, string afterPlaceHolder = "")
    {
        string envelopeText = envelopeNumber is null
            ? afterPlaceHolder
            : $" acondicionado no interior do invólucro de segurança lacrado nº {envelopeNumber}" + afterPlaceHolder;

        Utils.ReplaceText(document, placeHolder, envelopeText);
    }

    private void ReplaceWeaponDetails(DocX document, BalisticaRequest balisticaRequest)
    {
        Utils.ReplaceText(document, ReportPlaceholdersBalistica.Tipo, Utils.GetEnumMemberValue(balisticaRequest.WeaponType));
        Utils.ReplaceText(document, ReportPlaceholdersBalistica.Calibre, balisticaRequest.Caliber ?? "N/A");
        Utils.ReplaceText(document, ReportPlaceholdersBalistica.Marca, balisticaRequest.Brand ?? "N/A");
        Utils.ReplaceText(document, ReportPlaceholdersBalistica.Modelo, balisticaRequest.Model ?? "N/A");
        Utils.ReplaceText(document, ReportPlaceholdersBalistica.NumeroSerie, balisticaRequest.SerialNumber ?? "N/A");
    }

    private void ReplaceAdditionalDetails(DocX document, BalisticaRequest balisticaRequest)
    {
        Utils.ReplaceText(document, ReportPlaceholdersBalistica.Acabamento, Utils.GetEnumMemberValue(balisticaRequest.FinishWeapon));
        Utils.ReplaceText(document, ReportPlaceholdersBalistica.Alimentacao, Utils.GetEnumMemberValue(balisticaRequest.WeaponFeed));
        Utils.ReplaceText(document, ReportPlaceholdersBalistica.Carregador, Utils.GetEnumMemberValue(balisticaRequest.WeaponCharger));
        Utils.ReplaceText(document, ReportPlaceholdersBalistica.Capacidade, balisticaRequest.CapacityCharger.ToString());
        Utils.ReplaceText(document, ReportPlaceholdersBalistica.Soleira, Utils.GetEnumMemberValue(balisticaRequest.SoleiraArma));
        Utils.ReplaceText(document, ReportPlaceholdersBalistica.MedidaCano, balisticaRequest.PipeMeasurement ?? "N/A");
        Utils.ReplaceText(document, ReportPlaceholdersBalistica.MedidaTotal, balisticaRequest.TotalMeasure ?? "N/A");
    }

    private void ReplaceGenderPlaceholder(DocX document, Gender gender, string placeholder, string maleText, string femaleText)
    {
        string genderText = gender switch
        {
            Gender.Male => maleText,
            Gender.Female => femaleText,
            _ => throw new ArgumentException("Gênero inválido")
        };

        Utils.ReplaceText(document, placeholder, genderText);
    }

}
