import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../../types/member';
import { PaginatedResult } from '../../types/pagination';

@Injectable({
  providedIn: 'root',
})
export class LikesService {
  private baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  likeIds = signal<string[]>([]);

toggleLike(targetMemberId: string) {
    return this.http.post(`${this.baseUrl}likes/${targetMemberId}`, {});
    //.subscribe({
    //   next: (response) => {
    //     this.likeIds.update((ids) => [...ids, targetMemberId]);
    //   },
    //   error: (error) => {
    //     console.error('Error toggling like:', error);
    //   },
    // });

}
getLikes(predicate: string, pageNumber: number, pageSize: number) {

  let params=new HttpParams();
  params=params.append('predicate',predicate);
  params=params.append('pageNumber',pageNumber);
  params=params.append('pageSize',pageSize);
  
  return this.http.get<PaginatedResult<Member>>(this.baseUrl + 'likes',{params});
  }

  getLikeIds() {
    return this.http.get<string[]>(this.baseUrl + 'likes/list').subscribe({
      next: (response) => {
        this.likeIds.set(response);
      },
      error: (error) => {
        console.error('Error fetching like IDs:', error);
      },
    });
  }
  clearLikeIds() {   
     this.likeIds.set([]);
  }
}