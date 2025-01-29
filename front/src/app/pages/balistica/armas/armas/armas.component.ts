import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BalisticaRequest } from '../../../../types/balistica';
import { TipoBalistica } from '../../../../enums/balistica/tipo-balistica';
import { Gender } from '../../../../enums/gender';
import { TipoArma } from '../../../../enums/balistica/tipo-arma';
import { AcabamentoArma } from '../../../../enums/balistica/acabamento-arma';
import { AlimentacaoArma } from '../../../../enums/balistica/alimentacao-arma';
import { CarregadorArma } from '../../../../enums/balistica/carregador-arma';
import { SoleiraArma } from '../../../../enums/balistica/soleira-arma';
import { ResultadoExameBalistica } from '../../../../enums/balistica/resultado-exame-balistica';
import { STRCS } from '../../../../enums/strcs';

@Component({
  selector: 'app-armas',
  standalone: false,
  
  templateUrl: './armas.component.html',
  styleUrl: './armas.component.css'
})
export class ArmasComponent {
  currentStep = 1;
  form: FormGroup;
  selectedUnits: { [key: string]: string } = {};

  tipoBalisticaOptions = this.createEnumOptions(TipoBalistica);
  genderOptions = this.createEnumOptions(Gender);
  tipoArmaOptions = this.createEnumOptions(TipoArma);
  acabamentoOptions = this.createEnumOptions(AcabamentoArma);
  alimentacaoOptions = this.createEnumOptions(AlimentacaoArma);
  carregadorOptions = this.createEnumOptions(CarregadorArma);
  soleiraOptions = this.createEnumOptions(SoleiraArma);
  resultadoOptions = this.createEnumOptions(ResultadoExameBalistica);
  strcsOptions = this.createEnumOptions(STRCS)

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      passo1: this.fb.group({
        File: [null, Validators.required],
        //Gender: [Gender["Masculino"], Validators.required],
        //Amount: [1, [Validators.required, Validators.min(1)]],
        //BalisticType: [TipoBalistica['Arma de Fogo'], Validators.required],
        //STRCS: [STRCS['Pedra Azul'], Validators.required],
        EnvelopeNumber: [''],
        Caliber: [''],
        Brand: [''],
        PipeMeasurement: [''],
        TotalMeasure: ['']
      }),
      passo2: this.fb.group({
        Image: [null],
        Model: [''],
        SerialNumber: [''],
        WeaponType: ['', Validators.required],
        FinishWeapon: ['', Validators.required],
        SoleiraArma: ['', Validators.required],
        WeaponFeed: ['', Validators.required],  
        WeaponCharger: ['', Validators.required],
        CapacityCharger: ['', [Validators.required, Validators.min(1)]],
      }),
      passo3: this.fb.group({
        BallisticsExamResult: ['', Validators.required]
      })
    });
  }

  nextStep() {
    if (this.isCurrentStepValid()) {
      this.currentStep++;
    }
  }

  prevStep() {
    this.currentStep--;
  }

  isCurrentStepValid(): boolean {
    const stepGroup = this[`passo${this.currentStep}` as keyof this] as FormGroup;
    return stepGroup.valid;
  }

  onSubmit() {
    if (this.form.valid) {
      const formData: BalisticaRequest = {
        ...this.form.value.passo1,
        ...this.form.value.passo2,
        ...this.form.value.passo3
      };
      
      // Aqui você faria a submissão para o serviço
      console.log('Dados enviados:', formData);
      this.form.reset();
      this.currentStep = 1;
    }
  }

  onFileSelected(controlName: string, event: any) {
    const file = event.target.files[0];
    // No passo 1, atribuímos o arquivo ao controle "File"
    if (this.currentStep === 1) {
      this.form.get('passo1.File')?.setValue(file);
    }
  }

  // Adicione estas propriedades para acessar os grupos de forma tipada
get passo1(): FormGroup {
  return this.form.get('passo1') as FormGroup;
}

get passo2(): FormGroup {
  return this.form.get('passo2') as FormGroup;
}

get passo3(): FormGroup {
  return this.form.get('passo3') as FormGroup;
}

private createEnumOptions(enumObj: any): { value: any, label: string }[] {

    return Object.keys(enumObj)
      .filter(key => isNaN(Number(key)))
      .map(key => ({
        value: enumObj[key as keyof typeof enumObj],
        label: key
      }));
}

setUnit(field: string, unit: string) {
  this.selectedUnits[field] = unit;
}

onApprove() {
  // Atualiza o campo BallisticsExamResult para 'Aprovado'
  console.log('Aprovado!');
  this.passo3.get('BallisticsExamResult')?.setValue('Aprovado'); // Atualiza o valor
}

onReject() {
  // Atualiza o campo BallisticsExamResult para 'Rejeitado'
  console.log('Rejeitado!');
  this.passo3.get('BallisticsExamResult')?.setValue('Rejeitado'); // Atualiza o valor
}

onDamaged() {
  // Atualiza o campo BallisticsExamResult para 'Prejudicado'
  console.log('Prejudicado!');
  this.passo3.get('BallisticsExamResult')?.setValue('Prejudicado'); // Atualiza o valor
}

}
