import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TipoBalistica } from '../../enums/balistica/tipo-balistica';
import { Gender } from '../../enums/gender';
import { TipoArma } from '../../enums/balistica/tipo-arma';
import { AcabamentoArma } from '../../enums/balistica/acabamento-arma';
import { AlimentacaoArma } from '../../enums/balistica/alimentacao-arma';
import { CarregadorArma } from '../../enums/balistica/carregador-arma';
import { SoleiraArma } from '../../enums/balistica/soleira-arma';
import { ResultadoExameBalistica } from '../../enums/balistica/resultado-exame-balistica';
import { STRCS } from '../../enums/strcs';

@Component({
  selector: 'app-balistica',
  standalone: false,
  
  templateUrl: './balistica.component.html',
  styleUrl: './balistica.component.css'
})
export class BalisticaComponent {
  currentStep = 1;
  progressValue = 33;
  selectedUnitTotalLength: string | null = null;
  selectedUnitPipelLength: string | null = null;
  form: FormGroup;

  // Enum para usar nos selects
  tipoBalistica = Object.values(TipoBalistica).filter((key) => isNaN(Number(key)));
  genderOptions = Object.values(Gender).filter((key) => isNaN(Number(key)));;
  weaponTypes = Object.values(TipoArma).filter((key) => isNaN(Number(key)));;
  finishOptions = Object.values(AcabamentoArma).filter((key) => isNaN(Number(key)));;
  feedOptions = Object.values(AlimentacaoArma).filter((key) => isNaN(Number(key)));;
  chargerOptions = Object.values(CarregadorArma).filter((key) => isNaN(Number(key)));;
  soleiraOptions = Object.values(SoleiraArma).filter((key) => isNaN(Number(key)));;
  examResults = Object.values(ResultadoExameBalistica).filter((key) => isNaN(Number(key)));;
  strcsOptions = Object.values(STRCS).filter((key) =>isNaN(Number(key)));
  
  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      //Passo 1
      balisticType: [null, Validators.required],
      strcs: [null, Validators.required],

      //Passo 2
      file: [null, Validators.required],
      gender: [null, Validators.required],
      amount: [0, [Validators.required, Validators.min(1)]],
      envelopeNumber: [null],
      pipeMeasurement: [''],
      totalMeasure: [''],

      // Passo 3
      image: [null],
      caliber: [''],
      brand: [''],
      weaponType: [null, Validators.required],
      model: [''],
      serialNumber: [''],
      finishWeapon: [null, Validators.required],
      weaponFeed: [null, Validators.required],
      weaponCharger: [null, Validators.required],
      capacityCharger: [0, Validators.required],
      soleiraArma: [null, Validators.required],
      ballisticsExamResult: [null, Validators.required],
    })
  }

  // Avança para o próximo passo
  nextStep() {
    if (this.currentStep < 3) {
      this.currentStep++;
      this.updateProgress();
    }
  }

  selectUnitPipe(unit: string) {
    this.selectedUnitPipelLength = unit;
  }
  selectUnitTotal(unit: string) {
    this.selectedUnitTotalLength = unit;
  }
  // Retorna para o passo anterior
  previousStep() {
    if (this.currentStep > 1) {
      this.currentStep--;
      this.updateProgress();
    }
  }

  // Atualiza a barra de progresso
  updateProgress() {
    this.progressValue = Math.floor((this.currentStep / 3) * 100);
  }

  // Envia o formulário
  submitForm() {
    if (this.form.valid) {
      console.log(this.form.value);
      alert('Formulário enviado com sucesso!');
    } else {
      alert('Por favor, preencha todos os campos obrigatórios.');
    }
  }
  
}
