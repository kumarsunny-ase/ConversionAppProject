import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { Observable } from 'rxjs';
import { getAmountResponses } from '../models/get-amount.model';

@Component({
  selector: 'app-output',
  templateUrl: './output.component.html',
  styleUrls: ['./output.component.css'],
})
export class OutputComponent implements OnInit {
  getAmountResponse?: getAmountResponses;

  constructor(private userService: UserService) {}

  ngOnInit(): void {

    this.userService.getAmount().subscribe((result: getAmountResponses[]) => {
      this.getAmountResponse = result[0];
    });
  }
}
