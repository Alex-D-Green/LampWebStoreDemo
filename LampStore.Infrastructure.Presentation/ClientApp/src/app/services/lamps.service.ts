import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable()
export class LampsService
{
  private url = "http://localhost:58390/api/lamp";

  constructor(private http: HttpClient)
  {
  }

  getLamps()
  {
    return this.http.get(this.url);
  }
}
