using gen_fast_report.Attributes;
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
                document = ReplaceMainValues(document, balisticaRequest);

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

    private DocX ReplaceMainValues(DocX document, BalisticaRequest balisticaRequest)
    {
        document = ReplaceGenderSpecificValues(document, balisticaRequest.Gender);
        document = ReplaceBalisticaTypeValues(document, balisticaRequest);
        return document;
    }

    private DocX ReplaceGenderSpecificValues(DocX document, Gender gender)
    {
        string historicoText = gender switch
        {
            Gender.Male => "o Perito Criminal, signatário",
            Gender.Female => "a Perita Criminal, signatária",
            _ => throw new ArgumentException("Invalid gender")
        };

        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "#historico",
            NewValue = historicoText
        });

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
        switch (balisticaRequest.Gender) 
        {
            case Gender.Male:
                document.ReplaceText(new StringReplaceTextOptions
                {
                    SearchValue = "#CONSID1",
                    NewValue = "o perito"
                });
                break;
            case Gender.Female:
                document.ReplaceText(new StringReplaceTextOptions
                {
                    SearchValue = "#CONSID1",
                    NewValue = "a perito"
                });
                break;
        }

        switch (balisticaRequest.Amount) 
        {
            case 1:
                document.ReplaceText(new StringReplaceTextOptions
                {
                    SearchValue = "#CONSID2",
                    NewValue = "na peça acima discriminada"
                });
                break;
            default:
                document.ReplaceText(new StringReplaceTextOptions
                {
                    SearchValue = "#CONSID2",
                    NewValue = "nas peça acima discriminada"
                });
                break;
        }
        return document;
    }

    private DocX ReplaceMaterialDescriptionWeapon(DocX document, BalisticaRequest balisticaRequest)
    {
        //Envelope Number
        switch (balisticaRequest.EnvelopeNumber)
        {
            case null:
                document.ReplaceText(new StringReplaceTextOptions
                {
                    SearchValue = "#INVOLUCRO",
                    NewValue = ":"
                });
                break;

            default:
                document.ReplaceText(new StringReplaceTextOptions
                {
                    SearchValue = "#INVOLUCRO",
                    NewValue = $" acondicionado no interior do invólucro de segurança lacrado nº {balisticaRequest.EnvelopeNumber}:"
                });
                break;
        }
        //Weapon Ttype
        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "#TIPO",
            NewValue = EnumUtils.GetEnumMemberValue(balisticaRequest.WeaponType)
        });

        //Caliber
        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "#CALIBRE",
            NewValue = balisticaRequest.Caliber
        });

        //Brand
        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "#MARCA",
            NewValue = balisticaRequest.Brand
        });

        //Model
        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "#MODELO",
            NewValue = balisticaRequest.Model
        });

        //Serial Number
        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "#NUMEROSERIE",
            NewValue = balisticaRequest.SerialNumber
        });

        //Finish Weapon
        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "#ACABAMENTO",
            NewValue = EnumUtils.GetEnumMemberValue(balisticaRequest.FinishWeapon)
        });

        //Weapon Feed
        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "#ALIMENTACAO",
            NewValue = EnumUtils.GetEnumMemberValue(balisticaRequest.WeaponFeed)
        });

        //Weapon Charger
        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "#CARREGADOR",
            NewValue = EnumUtils.GetEnumMemberValue(balisticaRequest.WeaponCharger)
        });

        //Charger Capacity
        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "#CAPACIDADE",
            NewValue = balisticaRequest.CapacityCharger.ToString()
        });

        //Soleira
        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "#SOLEIRA",
            NewValue = EnumUtils.GetEnumMemberValue(balisticaRequest.SoleiraArma)
        });

        //Pipe Measurement
        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "#MEDIDACANO",
            NewValue = balisticaRequest.PipeMeasurement
        });

        //Total Measure
        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "#MEDIDATOTAL",
            NewValue = balisticaRequest.TotalMeasure
        });

        return document;
    }

}
