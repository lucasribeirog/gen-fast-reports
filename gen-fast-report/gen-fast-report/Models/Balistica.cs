﻿using gen_fast_report.Enums;

namespace gen_fast_report.Models
{
    public class Balistica
    {
        public  TipoBalistica Tipo;
        public string? IdPcnet { get; set; }
        public  string? Laudo { get; set; }
        public  string? Requisicao { get; set; }
        public  string? Reds { get; set; }
        public  string? UnidadeRequisitante { get; set; }
        public  string? AutoridadeRequisitante { get; set; }
        public  string? Pcnet { get; set; }
        public  string? ResponsavelPericia { get; set; }
        public  string? Fav { get; set; }
        public  string? DescricaoExame { get; set; }
        public  string? DataInicio { get; set; }
        public  string? HoraInicio { get; set; }

    }
}
