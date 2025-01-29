import { AcabamentoArma } from "../enums/balistica/acabamento-arma";
import { AlimentacaoArma } from "../enums/balistica/alimentacao-arma";
import { CarregadorArma } from "../enums/balistica/carregador-arma";
import { ResultadoExameBalistica } from "../enums/balistica/resultado-exame-balistica";
import { SoleiraArma } from "../enums/balistica/soleira-arma";
import { TipoArma } from "../enums/balistica/tipo-arma";
import { TipoBalistica } from "../enums/balistica/tipo-balistica";
import { Gender } from "../enums/gender";
import { STRCS } from "../enums/strcs";

export interface BalisticaRequest {
    File: File;//Passo 2
    Gender: Gender;//Passo 2
    BalisticType: TipoBalistica;//Passo 1
    STRCS: STRCS;//Passo 1
    Image?: File;//Passo 3
    Caliber?: string;//Passo 3
    Brand?: string;//Passo 3
    Amount: number;//Passo 2
    EnvelopeNumber?: number;//Passo 2
    WeaponType: TipoArma;//Passo 3
    Model?: string;//Passo 3
    SerialNumber?: string;//Passo 3
    FinishWeapon: AcabamentoArma;//Passo 3
    WeaponFeed: AlimentacaoArma;//Passo 3
    WeaponCharger: CarregadorArma;//Passo 3
    CapacityCharger: number;//Passo 3
    SoleiraArma: SoleiraArma;//Passo 3
    PipeMeasurement?: string;//Passo 2
    TotalMeasure?: string;//Passo 2
    BallisticsExamResult: ResultadoExameBalistica; //Passo 3
  }