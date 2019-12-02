import { Component, OnInit } from '@angular/core';
import { LampsService } from '../services/lamps.service'
import { Lamp } from '../models/Lamp'

@Component({
  selector: 'app-lamp',
  templateUrl: './lamp.component.html',
  providers: [LampsService]
})
export class LampComponent implements OnInit
{
  private lamps: Array<Lamp>;

  constructor(private lampsService: LampsService)
  {
    this.lamps = new Array<Lamp>();
  }

  ngOnInit()
  {
    this.lampsService.getLamps().subscribe((lamps: Lamp[]) => this.lamps = lamps);
  }
}
