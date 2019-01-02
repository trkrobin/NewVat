<template>
    <div id="mytable">
        <router-link to="/customers/new" class="btn btn-primary">New Customer</router-link>
  <v-client-table :data="tableData" :columns="columns" :options="options" theme="bootstrap3">
      <router-link slot="Action" slot-scope="props" :to="'/customers/edit/'+props.row.customerID" class="glyphicon glyphicon-pencil text-center"> Edit</router-link>
  </v-client-table>
</div>
</template>
<script>
import axios from 'axios'

export default {
    data(){
        return{
            columns:['Action', 'customerCode', 'customerName','email'],
            tableData:[],
            options:{
                sortable: ['customerCode', 'customerName'],
                filterable: ['customerCode', 'customerName','email']
            }
        }
    },
    methods:{
        edit(id){

        }
    },
    created(){
        axios.get('/api/customers').then(res=>{
            this.tableData=res.data;
        }).catch(error=>console.log(error))
    }
    
}
</script>
<style>
.customerNew{
    float:right;
}
</style>
