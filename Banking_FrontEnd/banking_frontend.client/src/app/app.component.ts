import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ApiService } from './services/api.service'; 

export interface fetchData {
  userId: number;
  firstName: string;
  lastName: string;
}
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})


export class AppComponent implements OnInit {
  data: any;
  loading = false;
  error: string | null = null;
  public Users: fetchData[] = [];
  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.fetchData();
  }


  fetchData(): void {
    this.loading = true;
    this.error = null;

    this.apiService.getAllUsers().subscribe({
      next: (response) => {
        console.log('API response:', response);
        this.Users = response.map((user: any) => ({
          userId: user.userId,
          firstName: user.firstName,
          lastName: user.lastName,
        }));
        this.loading = false;
      },
      error: (err) => {
        console.error('API error:', err);
        this.error = 'Error fetching data: ' + err.message;
        this.loading = false;
      }
    });
  }

  title = 'banking_frontend.client';
}
