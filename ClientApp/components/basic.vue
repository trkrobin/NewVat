<template>
    <div>
        <h4>Basic</h4>
        <hr>
        <div class="form-group">
            <label for="code">Code</label>
            <input type="text" class="form-control" id="code" >
        </div>
        <div class="form-group">
            <label for="name">Name</label>
            <input type="text" class="form-control" id="name" >
        </div>
        <div class="form-group">
            <label for="group">Group</label>
            <select id="group" class="form-control"  required >
            <option value=""></option>
            <option value="1">First</option>
            </select>
        </div>
        <div class="form-group">
            <label for="startDate">Start Date</label>
            <input type="text" class="form-control" id="startDate" >
        </div>
        <div class="form-group">
            <label for="tinno">TIN No</label>
            <input type="text" class="form-control" id="tinno" >
        </div>
        <div class="form-group">
            <label for="vatno">VAT Registration No</label>
            <input type="text" class="form-control" id="vatno" >
        </div>
    </div>
</template>
<script>
export default {
    
}
</script>
<style>

</style>
