import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthServiceService } from '../../Services/auth-service.service';

@Component({
  selector: 'app-home',
  imports: [RouterLink,RouterLinkActive],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  constructor(private router : Router,private authService : AuthServiceService){}

  logout() {
    this.authService.logout();

    this.router.navigate(['']);
  }
 
  totalHours: number = 40; // Example: total hours worked in a week
  totalAppointments: number = 25; // Example: total appointments

}
