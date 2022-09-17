import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
// import { Console } from 'console'
import { catchError, map, Observable } from 'rxjs';
import { Image, ImageToSubmit } from '../_interfaces/image.model';

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  private url: string = 'https://localhost:5001/api/images';

  constructor(private http: HttpClient) { }

  submitImage(image: ImageToSubmit) {
    this.http.post(this.url, image).subscribe({
      error: (err: HttpErrorResponse) => console.log(err)
    });
  }

  // submitImage(image: ImageToSubmit) {
  //   // return this.http.post<ImageToSubmit>(this.url + 'submit', image).pipe(
  //     console.log('here');
  //   return this.http.post<ImageToSubmit>(this.url, image).pipe(
  //     catchError((err) => {
  //       throw err;
  //     }))
  // }

  getImages(): Observable<ImageToSubmit[]>{
   return this.http.get<ImageToSubmit[]>(this.url).pipe(
    catchError((err) => {
      throw err;
    }))
  }

  image
    // this.http.get(this.url)
    // .subscribe({
    //   next: (res) => this.images = res as Image[],
      
    //   error: (err: HttpErrorResponse) => console.log(err)
    // });
  
}
