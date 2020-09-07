import { Component } from '@angular/core';
import { VideoService } from 'src/app/services/video.service';
import { ColDef } from 'ag-grid-community';
import { ActivatedRoute, Router } from '@angular/router';
import { Videos } from 'src/app/models/videos';
import { BtnCellRendererComponent } from '../../renderers/button-renderer/btn.renderer';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-videos',
    templateUrl: './video-list.component.html'
})
export class VideoListComponent {
    selectedVideoId: string;
    addingVideo = false;
    videos: Videos[];
    defaultColDef = {
        flex: 1,
        minWidth: 150,
        resizable: true,
    };

    columnDefs: ColDef[] = [
        { hide: true, field: 'referenceId' },
        { headerName: 'Url', field: 'url', sortable: true, filter: true },
        { headerName: 'Title', field: 'title', sortable: true, filter: true },
        { headerName: 'Description', field: 'description', sortable: true, filter: true },
        { headerName: 'Extra Info', field: 'extraInfo', sortable: true, filter: true }
    ];

    private gridApi;

    constructor(private _videoService: VideoService, private _route: ActivatedRoute, private _router: Router) {
        this._route.params.subscribe(params => { this.selectedVideoId = params['videoId']; });
        if (this.selectedVideoId) {
            this.addingVideo = true;
        }
        const _this = this;
        this.getAllVideos();

        this.columnDefs.push({
            headerName: '',
            field: 'referenceId',
            cellRendererFramework: BtnCellRendererComponent,
            cellRendererParams: {
                clicked: function (isDelete: boolean, referenceId: any) {
                    if (isDelete) {
                        _this.deleteVideo(referenceId);
                    } else {
                        _this._router.navigate([`videos/${referenceId}`]);
                    }

                }
            },
        });
    }

    onGridReady(params) {
        this.gridApi = params.api;
    }

    getAllVideos() {
        this._videoService.getAllVideos().subscribe(videos => {
            this.videos = videos;
        });
    }

    deleteVideo(referenceId: string) {
        this._videoService.deleteVideo(referenceId).subscribe(x => {
            this.videos = this.videos.filter(g => g.referenceId !== referenceId);
            this.gridApi.setRowData(this.videos);
        });
    }

    addVideo() {
        this.addingVideo = true;
    }

    showAllVideos(showVideo: boolean) {
        this.addingVideo = false;
    }
}
