import { Component } from '@angular/core';
import { GroupService } from 'src/app/services/group.service';
import { Group } from 'src/app/models/group';
import { ColDef } from 'ag-grid-community';
import { ActivatedRoute, Router } from '@angular/router';
import { BtnCellRendererComponent } from '../../renderers/button-renderer/btn.renderer';

@Component({
    selector: 'app-groups',
    templateUrl: './group-list.component.html'
})
export class GroupListComponent {
    selectedGroupId: string;
    addingGroup = false;
    groups: Group[];
    defaultColDef = {
        flex: 1,
        minWidth: 150,
        resizable: true,
    };
    columnDefs: ColDef[] = [
        { hide: true, field: 'referenceId' },
        { headerName: 'Name', field: 'name', sortable: true, filter: true },
        { headerName: 'Email', field: 'email', sortable: true, filter: true },
        { headerName: 'Phone Number', field: 'phoneNumber', sortable: true, filter: true },
        { headerName: 'Contact Persom', field: 'contactPerson', sortable: true, filter: true },
        { headerName: 'Address', field: 'address', sortable: true, filter: true },
        // { headerName: 'Web Site', field: 'webSiteUrl', sortable: true , filter: true},
        // { headerName: 'Created Date', field: 'createdDate', sortable: true },
        // { headerName: 'Modified Date', field: 'modifiedDate', sortable: true }
    ];

    private gridApi;

    constructor(private _groupService: GroupService, private _route: ActivatedRoute, private _router: Router) {
        this._route.params.subscribe(params => { this.selectedGroupId = params['groupId']; });
        if (this.selectedGroupId) {
            this.addingGroup = true;
        }
        const _this = this;
        this.getAllGroups();

        this.columnDefs.push({
            headerName: '',
            field: 'referenceId',
            cellRendererFramework: BtnCellRendererComponent,
            cellRendererParams: {
                clicked: function (isDelete: boolean, referenceId: any) {
                    if (isDelete) {
                        _this.deleteGroup(referenceId);
                    } else {
                        _this._router.navigate([`groups/${referenceId}`]);
                    }

                }
            },
        });
    }

    onGridReady(params) {
        this.gridApi = params.api;
    }

    getAllGroups() {
        this._groupService.getAllGroups().subscribe(groups => {
            this.groups = groups;
        });
    }

    deleteGroup(referenceId: string) {
        this._groupService.deleteGroup(referenceId).subscribe(x => {
            this.groups = this.groups.filter(g => g.referenceId !== referenceId);
            this.gridApi.setRowData(this.groups);
        });
    }

    addGroup() {
        this.addingGroup = true;
    }

    showAllGroups() {
        this.addingGroup = false;
    }
}
