import { Component, OnInit } from '@angular/core';
import { ImageToSubmit } from './_interfaces/image.model';
import { ImageService } from './_service/image.service';
import { interval, tap } from 'rxjs';
import { WebcamImage } from 'ngx-webcam';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  isCreate: boolean;
  name: string;
  description: string;
  image: ImageToSubmit;
  // images: Image[] = [];
  images: ImageToSubmit[] = [];
  response: {dbPath: ''};
  photos: string[] = [];
  sanitizeImgPath: string = "";
  webcamImage: WebcamImage | undefined;

  constructor(
    private imageService: ImageService,
    ){}

  ngOnInit(){
    this.isCreate = true;
    this.getPhotos();

    interval(60000).pipe(
      tap(() => this.getPhotos()),
    ).subscribe(() => console.log('FINISHED!'));
  }

  private getPhotos = () => {
    this.imageService.getImages().subscribe(images => {
      this.images = images;
      console.log(this.images);
    })
  }

  onCreate = () => {
    this.image = {
      name: this.name,
      description: this.description,
      imgPath: this.response.dbPath
    }
    this.imageService.submitImage(this.image);
    this.images.push(this.image);
    this.returnToCreate();
  }


  returnToCreate = () => {
    this.isCreate = true;
    this.name = '';
    this.description  = '';
  
  }

  uploadFinished = (event) => { 
    this.response = event; 
  }

  handleImage(webcamImage: WebcamImage) {
    this.webcamImage = webcamImage;
  }




}
