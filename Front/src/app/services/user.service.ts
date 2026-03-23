import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Address {
  street: string;
  isPrimary: boolean;
}

export interface User {
  id: string;
  fullName: string;
  birth: string;
  email: string;
  addresses: Address[];
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
private apiUrl = 'http://localhost:5227/api/User';

    constructor(private http: HttpClient) {}

    getUsers(page: number = 1, pageSize: number = 10): Observable<User[]> {
        return this.http.get<User[]>(`${this.apiUrl}?page=${page}&pageSize=${pageSize}`);
    }

    createUser(user: any) {
        return this.http.post(this.apiUrl, user);
    }
    deleteUser(id: string) {
        return this.http.delete(`${this.apiUrl}/${id}`);
    }
    updateUser(id: string, user: any) {
        return this.http.put(`${this.apiUrl}/${id}`, user);
    }
}