import { Component, OnDestroy, OnInit } from '@angular/core';

import { UserService } from '../services/user.service';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { InputDto } from '../models/input-number-request.model';
import { getAmountResponses } from '../models/get-amount.model';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent implements OnDestroy, OnInit {
  inputNumber: number | undefined;
  getAmountResponse?: getAmountResponses;

  private userSubscription?: Subscription;

  constructor(private userService: UserService, private router: Router, private route: ActivatedRoute) {}
  ngOnInit(): void {
    this.userService.getAmount().subscribe((result: getAmountResponses[]) => {
      this.getAmountResponse = result[0];
    });
  }

  onFormSubmit() {
    this.userService
      .inputAmount(this.inputNumber!)
      .subscribe((result: InputDto) => {
        if(this.router.url === '/admin/output') {
           window.location.reload();
        } else
        {
          this.router.navigateByUrl(`/admin/output`);
        }              
        
      });
  }

  ngOnDestroy(): void {
    this.userSubscription?.unsubscribe();
  }
}
