import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthServiceService } from '../../Services/auth-service.service';

@Component({
  selector: 'app-patient-home',
  imports: [RouterLink,RouterLinkActive],
  templateUrl: './patient-home.component.html',
  styleUrl: './patient-home.component.css'
})
export class PatientHomeComponent {
  constructor(private router : Router,private authService : AuthServiceService){}

  logout() {
    this.authService.logout();

    this.router.navigate(['']);
  }
  staffName: string = 'John Doe'; // Should come from backend or service
  staffContact: string = '+91 9876543210'; // Example contact number

  // Modification Details
  modifiedName: string = 'Dr. Emily Smith'; // Doctor's name needing modification
  modifiedContact: string = '+91 8765432109';

}
