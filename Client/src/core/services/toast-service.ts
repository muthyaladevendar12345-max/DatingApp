import { Injectable } from '@angular/core';
import { isNotFound } from '@angular/core/primitives/di';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  constructor() {
    this.createToastContainer();
  }

  private createToastContainer() {
    if (!document.getElementById('toast-container')) {
      const container = document.createElement('div');
      container.id = 'toast-container';
      container.className = 'toast toast-bottom-right toast-end';
      document.body.appendChild(container);
    }
  }
  private createToastElement(message: string,alertclass: string, duration: number) {
    const toastContainer  = document.getElementById('toast-container');
    if(!toastContainer) return;

    const toast = document.createElement('div');
    toast.classList.add('alert', alertclass,'shadow-lg');
    toast.innerHTML = `
    <span>${message}</span>
    <button class="ml-4 btn btn-sm btn-ghost">✕</button>`;

    toast.querySelector('button')?.addEventListener('click', () => {  
    toastContainer.removeChild(toast);
    })
    toastContainer.appendChild(toast);

    setTimeout(() => {
      if (toastContainer.contains(toast)) {
        toastContainer.removeChild(toast);
      }
    }, duration);
  }
 success(message: string, duration: number = 5000) {
    this.createToastElement(message, 'alert-success', duration);
  }
  error(message: string, duration: number = 5000) { 
    this.createToastElement(message, 'alert-error', duration);
  }
  warming(message: string, duration: number = 5000) {
    this.createToastElement(message, 'alert-warning', duration);
  }
  info(message: string, duration: number = 5000) {
    this.createToastElement(message, 'alert-info', duration);
  }
}