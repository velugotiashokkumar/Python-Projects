import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DoctortodaysappserviceService } from '../../Services/doctortodaysappservice.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-doctorstodaysapp',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './doctorstodaysapp.component.html',
  styleUrl: './doctorstodaysapp.component.css'
})
export class DoctorstodaysappComponent {
  doctorId!: number;
  appointments: any[] = [];
 

  constructor(private doctorAppointmentsService: DoctortodaysappserviceService, private router: Router, private cd: ChangeDetectorRef) {}
 
  ngOnInit(): void {
    // ✅ Auto-assign doctor ID from local storage if available
    const storedRoleId = localStorage.getItem('roleId');
    if (storedRoleId) {
      this.doctorId = Number(storedRoleId); // Dynamically assign value
    }
 
    // ✅ Auto-trigger Get Appointments function
    this.fetchTodaysAppointments();
  }
 
  fetchTodaysAppointments(): void {
    if (!this.doctorId) {
      alert("Doctor ID not found in local storage.");
      this.router.navigate(['app-home']);
      return;
    }
 
    this.doctorAppointmentsService.getTodaysAppointmentsByDoctor(this.doctorId).subscribe({
      next: (response: any) => {
        console.log("Doctor's Appointments Today:", response);
        this.appointments = response.$values ?? [];
       
        this.cd.detectChanges();
      },
      error: (error: any) => {
        console.error("Error fetching appointments:", error);
        if (error.status === 404) {
          alert("No appointments found for this doctor today.");
          this.router.navigate(['app-home']);
        } else {
          alert("Failed to retrieve today's appointments. Please try again.");
        }
      }
    });
  }
 
  goBack(): void {
    this.router.navigate(['app-home']);
  }
 
}
