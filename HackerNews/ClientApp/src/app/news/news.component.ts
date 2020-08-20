import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { StoryItem } from "../Interfaces/story-item";

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {
  stories: StoryItem[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<StoryItem[]>(baseUrl + 'NewsStories/GetNewStories').subscribe(result => {
      this.stories = result;
      console.log(result);
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
