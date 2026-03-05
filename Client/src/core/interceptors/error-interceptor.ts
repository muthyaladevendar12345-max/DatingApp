import { HttpInterceptorFn } from '@angular/common/http';
import { throwError } from 'rxjs/internal/observable/throwError';
import { catchError } from 'rxjs/internal/operators/catchError';
import { ToastService } from '../services/toast-service';
import { inject } from '@angular/core/primitives/di';
import { Router } from '@angular/router';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toast = inject(ToastService); // Assuming you have a ToastService for notifications
  const router = inject(Router); // Assuming you have a Router for navigation
  return next(req).pipe(
    catchError((error) => {
      if (error) {
        switch (error.status) {
          case 400:
            // Handle bad request error, e.g., display validation errors
            if (error.error.errors) {
              const modalstateerrors: string[] = [];
              for (const key in error.error.errors) {
                if (error.error.errors[key]) {
                  modalstateerrors.push(error.error.errors[key]);
                }
              }
              throw modalstateerrors.flat();

            }
            toast.error(error.error);
            break;

          case 401:
            toast.error('Unauthorized: Please log in to continue');
            router.navigate(['/login']); // Redirect to login page
            break;
          case 404:
            toast.error('Not Found: The requested resource was not found');
            break;
          case 500:
            toast.error('Server Error: An unexpected error occurred on the server');
            break;
          default:
            toast.error('An unexpected error occurred');
        }
        // Handle unauthorized error, e.g., redirect to login page
        // You can also display a notification to the user
      }
      // Handle other types of errors as needed
      return throwError(() => error);
    })
  );
};
