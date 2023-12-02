import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { getAmountResponses } from '../models/get-amount.model';
import { environment } from 'src/environments/environment';
import { InputDto } from '../models/input-number-request.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  inputAmount(inputNumber: number): Observable<InputDto> {
    const formattedInput = inputNumber.toString().replace(',', '.');
    let result: Observable<InputDto> = this.http.post<InputDto>(
      `https://localhost:7123/api/Convertion/Create?inputNumber=${formattedInput}`,
      this.httpOptions
    );
    return result;
  }

  getAmount(): Observable<getAmountResponses[]> {
    return this.http.get<getAmountResponses[]>(
      `https://localhost:7123/api/Convertion`,
      this.httpOptions
    );
  }
}
