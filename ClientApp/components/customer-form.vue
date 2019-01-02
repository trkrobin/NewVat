<template>
    <div class="col-md-12">
        <form>
        <router-link class="btn btn-default pull-right" to="/customers">Back to list</router-link>
        <h3 v-if="!customer.customerID" style="color:blue">New Customer</h3>
        <h3 v-else style="color:blue">Edit Customer</h3>
        <hr style="border-color:red">
        <div class="col-md-6">
            <h4 class="section">Basic</h4>
            <hr>
            <div class="form-group">
                <label for="code" >Code</label>
                <input v-validate="'required'" name="code" type="text" class="form-control" id="code" v-model="customer.customerCode">
                <span class="trkerror" v-show="errors.has('code')">Customer code is required </span>
            </div>
            <div class="form-group">
                <label for="name">Name</label>
                <input type="text" class="form-control" id="name" v-model="customer.customerName">
            </div>
            <div class="form-group">
                <label for="group">Group</label>
                <select id="group" class="form-control"  required v-model="customer.customerGroupID">
                <option v-for="group in cGroups" :value="group.customerGroupID">{{group.customerGroupName}}</option>
                </select>
            </div>
            <div class="form-group">
                <label for="startDate">Start Date</label>
                <datepicker v-model="customer.startDateTime" name="startDate"></datepicker>
            </div>
            <div class="form-group">
                <label for="tinno">TIN No</label>
                <input type="text" class="form-control" id="tinno" v-model="customer.tinNo">
            </div>
            <div class="form-group">
                <label for="vatno">VAT Registration No</label>
                <input type="text" class="form-control" id="vatno" v-model="customer.vatRegistrationNo">
            </div>
            </div>
        <div class="col-md-6">
            <h4 class="section">Contact Person</h4>
            <hr>
            <div class="form-group">
                <label for="conName">Name</label>
                <input type="text" class="form-control" id="conName" v-model="customer.contactPerson">
            </div>
            <div class="form-group">
                <label for="conDesignation">Designation</label>
                <input type="text" class="form-control" id="conDesignation" v-model="customer.contactPersonDesignation">
            </div>
            <div class="form-group">
                <label for="conTelephone">Telephone No</label>
                <input type="text" class="form-control" id="conTelephone" v-model="customer.contactPersonTelephone">
            </div>
            <div class="form-group">
                <label for="conEmail">Email</label>
                <input v-validate="'email'" name="cEmail" type="text" class="form-control" id="conEmail" v-model="customer.contactPersonEmail">
                <span class="trkerror" v-show="errors.has('cEmail')">You must enter a valid email </span>
            </div>
            <h4 class="section">Business</h4>
            <hr>
            <div class="form-group">
                <label for="businessType">Business Type</label>
                <input type="text" class="form-control" id="businessType" v-model="customer.businessType">
            </div>
            <div class="form-group">
                <label for="businessCode">Business Code</label>
                <input type="text" class="form-control" id="businessCode" v-model="customer.businessCode">
            </div>
        </div>
        <div class="clear-fix"></div>
            <h4 class="section">Details</h4>
            <hr>
            <div class="form-group col-md-6">
                <label for="telephoneNo">Telephone No</label>
                <input type="text" class="form-control" id="telephoneNo" v-model="customer.telephoneNo">
            </div>
            <div class="form-group col-md-6">
                <label for="fax">Fax</label>
                <input type="text" class="form-control" id="fax" v-model="customer.faxNo">
            </div>
            <div class="form-group col-md-6">
                <label for="email">Email</label>
                <input type="text" class="form-control" id="email" v-model="customer.email">
            </div>
            <div class="form-group col-md-6">
                <label for="adress">Address</label>
                <input type="text" class="form-control" id="adress" v-model="customer.address1">
            </div>
            <div class="form-group col-md-6">
                <label for="city">City</label>
                <input type="text" class="form-control" id="city" v-model="customer.city">
            </div>
            <div class="form-group col-md-6">
                <label for="country">Country</label>
                <input type="text" class="form-control" id="country" v-model="customer.country">
            </div>
            <div class="form-group col-md-12">
                <label for="comments">Comments</label>
                <input type="text" class="form-control" id="comments" v-model="customer.comments">
            </div>
            <div class="form-group col-md-4">
                <toggle-button 
                v-model="customer.activeStatus"
                color="#1dd2f2"
                /><span class="trkmarl10">{{activeStatus}}</span>
                <!-- <label for="activeStatus">
                    <input type="checkbox" id="activeStatus"  checked="true" v-model="customer.activeStatus"> Is active?
                </label> -->
            </div>
            <button v-if="!customer.customerID" class="btn btn-success" type="button" @click="saveCustomer">Save</button>
            <button v-else class="btn btn-success" type="button" @click="updateCustomer">Update</button>
        </form>
    </div>
</template>

<script>
    import Basic from './basic.vue';
    import Contact from './contact-info.vue';
    import axios from 'axios';
    import Datepicker from 'vuejs-datepicker';

    export default {
        mounted(){
            var sDate=document.getElementsByName("startDate")[0];
            sDate.classList.add("form-control");
            sDate.placeholder="Click here to select date";
        },
        components:{
            'appBasic':Basic,
            'appContact':Contact,
            Datepicker
        },
        computed:{
            activeStatus(){
                return this.customer.activeStatus?" Active":" Not Active";
            }
        },
        methods:{
            saveCustomer(){
                axios.post('/api/customers',this.customer).then(res=>{
                    console.log(res)
                }).catch(err=>console.log(err))
            },
            updateCustomer(){
                // this.$toastr.s("SUCCESS MESSAGE");
            },
            customFormatter(date) {
                return moment(date).format('MMMM Do YYYY, h:mm:ss a');
            }
        },
        created(){
            axios.get('/api/customerGroups').then(res=>{
                this.cGroups=res.data;
            }).catch(err=>console.log(err));

            var id=this.$route.params.id||0;
            if(id>0){
                axios.get('/api/customers/'+id).then(res=>{
                    this.customer=res.data;
                    console.log(this.customer.customerGroupID);
                }).catch(error=>console.log(error))
            }
            
        },
      data () {
        return {
          cGroups:[],
          customer:{
              customerID:0,
              customerCode:'',
              customerName:'',
              customerGroupID:'',
              address1:'',
              address2:'',
              address3:'',
              city:'',
              telephoneNo:'',
              faxNo:'',
              email:'',
              tinNo:'',
              vatRegistrationNo:'',
              comments:'',
              activeStatus:true,
              createdBy:'',
              createdOn:'',
              lastModifiedBy:'',
              lastModifiedOn:'',
              startDateTime:'',
              contactPerson:'',
              contactPersonDesignation:'',
              contactPersonTelephone:'',
              contactPersonEmail:'',
              country:'',
              vdsPercent:0,
              businessType:'',
              businessCode:''
          }
        }
      }
    }
</script>

<style>
.section{
    color:indigo;
    margin-bottom: -10px;
}
form{
    padding-bottom: 50px;
}
.trkmarl10{
    margin-left: 10px;
}
.trkerror{
    color: red;
}

</style>
