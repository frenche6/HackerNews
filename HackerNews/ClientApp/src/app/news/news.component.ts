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
  filteredStories: StoryItem[];
  paginationConfig: any;
  searchText: string;

  httpClient: HttpClient;
  baseUrl: string;

  itemsPerPage: number = 10;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpClient = http;
    this.baseUrl = baseUrl;    
  }

  ngOnInit() {
    this.getNewsStories();
  }

  getNewsStories(){
    this.httpClient.get<StoryItem[]>(this.baseUrl + 'NewsStories/GetNewStories').subscribe(result => {
      this.stories = result;
      this.filteredStories = result;
      this.setPaginationConfig(this.itemsPerPage, 1, this.stories.length);
    }, error => console.error(error));
  }

  pageChanged(event){
    this.paginationConfig.currentPage = event;
  }

  setPaginationConfig(itemsPerPage: number, currentPage: number, totalItems: number){
    this.paginationConfig = {
      itemsPerPage: itemsPerPage,
      currentPage: currentPage,
      totalItems: totalItems
    };
  }

  onKey(event: any){
    this.filteredStories = this.stories.filter(story => story != null && story.title != null && story.title.includes(event.target.value));
    this.setPaginationConfig(this.itemsPerPage, 1, this.filteredStories.length);
  }

}
