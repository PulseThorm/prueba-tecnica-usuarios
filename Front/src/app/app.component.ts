import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService, User } from './services/user.service';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit {
  title = 'UserFront';
  users: User[] = [];
  loading = false;
  error = '';

  isCreateModalOpen = false;

  newUser = {
    fullName: '',
    birth: '',
    email: '',
    addresses: [
      {
        street: '',
        isPrimary: true
      }
    ]
  };

  isEditModalOpen = false;

  editUser = {
    id: '',
    fullName: '',
    birth: '',
    email: '',
    addresses: [] as { street: string; isPrimary: boolean }[],
    newAddresses: [] as { street: string; isPrimary: boolean }[]
  };
  
  currentPage = 1;
  pageSize = 10;
  hasNextPage = false;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.loading = true;
    this.error = '';

    this.userService.getUsers(this.currentPage, this.pageSize).subscribe({
      next: (response) => {
        this.users = response;
        this.hasNextPage = response.length === this.pageSize;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error al cargar usuarios:', err);
        this.error = 'No se pudieron cargar los usuarios.';
        this.loading = false;
      }
    });
  }

  goToNextPage(): void {
    if (this.hasNextPage) {
      this.currentPage++;
      this.loadUsers();
    }
  }

  goToPreviousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadUsers();
    }
  }

  setPrimary(index: number): void {
    this.newUser.addresses.forEach((addr, i) => {
      addr.isPrimary = i === index;
    });
  }

  getPrimaryAddress(user: User): string {
    const primary = user.addresses?.find(address => address.isPrimary);
    return primary ? primary.street : 'Sin dirección principal';
  }

  onCreate(): void {
    this.newUser = {
      fullName: '',
      birth: '',
      email: '',
      addresses: []
    };

    this.isCreateModalOpen = true;
  }

  closeCreateModal(): void {
    this.isCreateModalOpen = false;
  }

  onEdit(user: User): void {
    this.editUser = {
      id: user.id,
      fullName: user.fullName,
      birth: user.birth ? user.birth.split('T')[0] : '',
      email: user.email,
      addresses: user.addresses ? [...user.addresses] : [],
      newAddresses: []
    };

    this.isEditModalOpen = true;
  }

  closeEditModal(): void {
    this.isEditModalOpen = false;
  }
  
  addEditAddress(): void {
    this.editUser.newAddresses.push({
      street: '',
      isPrimary: false
    });
  }

  setEditPrimary(index: number): void {
    this.editUser.newAddresses.forEach((addr, i) => {
      addr.isPrimary = i === index;
    });
  }

  onDelete(user: User): void {
    Swal.fire({
      title: '¿Eliminar usuario?',
      text: `Se eliminará a ${user.fullName}`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.userService.deleteUser(user.id).subscribe({
          next: () => {
            Swal.fire({
              title: 'Eliminado',
              text: 'El usuario fue eliminado correctamente',
              icon: 'success',
              confirmButtonText: 'Aceptar'
            });

            this.loadUsers();
          },
          error: (err) => {
            console.error('Error al eliminar usuario:', err);

            Swal.fire({
              title: 'Error',
              text: 'No se pudo eliminar el usuario',
              icon: 'error',
              confirmButtonText: 'Aceptar'
            });
          }
        });
      }
    });
  }

  addAddress(): void {
    this.newUser.addresses.push({
      street: '',
      isPrimary: false
    });
  }
  
  createUser(): void {
    const payload = {
      ...this.newUser,
      birth: this.newUser.birth + 'T00:00:00'
    };

    console.log('Payload enviado:', payload);

    this.userService.createUser(payload).subscribe({
      next: () => {
        Swal.fire({
          title: 'Usuario creado correctamente',
          text: 'El usuario fue creado correctamente',
          icon: 'success',
          confirmButtonText: 'Aceptar'
        });
        this.closeCreateModal();
        this.loadUsers();
      },
      error: (err) => {
        console.error('Error al crear usuario:', err);
         Swal.fire({
          title: 'Error',
          text: 'No se pudo crear el usuario',
          icon: 'error',
          confirmButtonText: 'Aceptar'
        });
      }
    });
  }

  updateUser(): void {
    const payload = {
      fullName: this.editUser.fullName,
      birth: this.editUser.birth + 'T00:00:00',
      email: this.editUser.email,
      addresses: this.editUser.newAddresses.filter(a => a.street.trim() !== '')
    };

    console.log('Payload edición enviado:', payload);

    this.userService.updateUser(this.editUser.id, payload).subscribe({
      next: () => {
        Swal.fire({
          title: 'Actualizado',
          text: 'El usuario fue actualizado correctamente',
          icon: 'success',
          confirmButtonText: 'Aceptar'
        });

        this.closeEditModal();
        this.loadUsers();
      },
      error: (err) => {
        console.error('Error al actualizar usuario:', err);

        Swal.fire({
          title: 'Error',
          text: 'No se pudo actualizar el usuario',
          icon: 'error',
          confirmButtonText: 'Aceptar'
        });
      }
    });
  }
}