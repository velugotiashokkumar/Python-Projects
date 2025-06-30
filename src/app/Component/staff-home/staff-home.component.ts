import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive} from '@angular/router';
import { AuthServiceService } from '../../Services/auth-service.service';
 
@Component({
  selector: 'app-staff-home',
  standalone: true,
  imports: [RouterLink,CommonModule,RouterLinkActive],
  templateUrl: './staff-home.component.html',
  styleUrl: './staff-home.component.css'
})
export class StaffHomeComponent {
  constructor(private router : Router,private authService : AuthServiceService){}

  logout() {
    this.authService.logout();

    this.router.navigate(['']);
  }
 
}