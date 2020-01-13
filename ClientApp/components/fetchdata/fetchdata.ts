import Vue from 'vue';
import { GridPlugin, Edit, Toolbar } from '@syncfusion/ej2-vue-grids';
import { DataManager, UrlAdaptor } from '@syncfusion/ej2-data';
Vue.use(GridPlugin);

export default Vue.extend({
    data(){
        return{
            data: new DataManager({
                url: 'Home/UrlDatasource',
                insertUrl: 'Home/Insert',
                updateUrl: 'Home/Update',
                removeUrl: 'Home/Delete',
                // crudUrl: 'Home/CrudAction',
                // batchUrl: 'Home/BatchUpdate',
                adaptor: new UrlAdaptor()
            }),
            editOptions:{
                allowAdding: true,
                allowEditing: true,
                allowDeleting: true,
                // mode: "Batch" // Enable while performing batch editing
            },
            toolbarOptions: ['Add', 'Edit', 'Delete', 'Update', 'Cancel']
        }
    },
    provide:{
        grid: [Edit, Toolbar]
    }
})