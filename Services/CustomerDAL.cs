using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using newvat.ViewModels;


namespace newvat.Services
{
    public class CustomerDAL
    {
        #region Global Variables
        private const string FieldDelimeter = DBConstant.FieldDelimeter;
        private DBSQLConnection _dbsqlConnection = new DBSQLConnection();
        #endregion
        #region Methods

        //==================DropDown=================
        public List<CustomerGroupVM> DropDown()
        {
            #region Variables
            SqlConnection currConn = null;
            string sqlText = "";
            List<CustomerGroupVM> VMs = new List<CustomerGroupVM>();
            CustomerGroupVM vm;
            #endregion
            try
            {
                #region open connection and transaction
                currConn = _dbsqlConnection.GetConnection();
                if (currConn.State != ConnectionState.Open)
                {
                    currConn.Open();
                }
                #endregion open connection and transaction
                #region sql statement
                sqlText = @"
SELECT
af.CustomerGroupID
,af.CustomerGroupName
FROM CustomerGroups af
WHERE  1=1
";


                SqlCommand objComm = new SqlCommand(sqlText, currConn);

                SqlDataReader dr;
                dr = objComm.ExecuteReader();
                while (dr.Read())
                {
                    vm = new CustomerGroupVM();
                    vm.CustomerGroupID = dr["CustomerGroupID"].ToString();
                    vm.CustomerGroupName = dr["CustomerGroupName"].ToString();
                    VMs.Add(vm);
                }
                dr.Close();
                #endregion
            }
            #region catch
            catch (SqlException sqlex)
            {
                throw new ArgumentNullException("", "SQL:" + sqlText + FieldDelimeter + sqlex.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("", "SQL:" + sqlText + FieldDelimeter + ex.Message.ToString());
            }
            #endregion
            #region finally
            finally
            {
                if (currConn != null)
                {
                    if (currConn.State == ConnectionState.Open)
                    {
                        currConn.Close();
                    }
                }
            }
            #endregion
            return VMs;
        }

        //==================SelectAll=================
        public List<CustomerVM> SelectAll(string Id = null, string[] conditionFields = null, string[] conditionValues = null, SqlConnection VcurrConn = null, SqlTransaction Vtransaction = null)
        {
            #region Variables
            SqlConnection currConn = null;
            SqlTransaction transaction = null;
            string sqlText = "";
            List<CustomerVM> VMs = new List<CustomerVM>();
            CustomerVM vm;
            #endregion
            try
            {
                #region open connection and transaction
                #region New open connection and transaction
                if (VcurrConn != null)
                {
                    currConn = VcurrConn;
                }
                if (Vtransaction != null)
                {
                    transaction = Vtransaction;
                }
                #endregion New open connection and transaction
                if (currConn == null)
                {
                    currConn = _dbsqlConnection.GetConnection();
                    if (currConn.State != ConnectionState.Open)
                    {
                        currConn.Open();
                    }
                }
                if (transaction == null)
                {
                    transaction = currConn.BeginTransaction("");
                }
                #endregion open connection and transaction
                #region sql statement
                #region SqlText

                sqlText = @"
SELECT
 CustomerID
,CustomerCode
,CustomerName
,CustomerGroupID
,Address1
,Address2
,Address3
,City
,TelephoneNo
,FaxNo
,Email
,StartDateTime
,ContactPerson
,ContactPersonDesignation
,ContactPersonTelephone
,ContactPersonEmail
,TINNo
,VATRegistrationNo
,Comments
,ActiveStatus
,CreatedBy
,CreatedOn
,LastModifiedBy
,LastModifiedOn
,Info2
,Info3
,Info4
,Info5
,Country
,ISNULL(VDSPercent,0) VDSPercent 
,BusinessType
,BusinessCode

FROM Customers  
WHERE  1=1 AND ActiveStatus = 'Y'

";
                if (Id !=null)
                {
                    sqlText += @" and CustomerID=@CustomerID";
                }

                string cField = "";
                if (conditionFields != null && conditionValues != null && conditionFields.Length == conditionValues.Length)
                {
                    for (int i = 0; i < conditionFields.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(conditionFields[i]) || string.IsNullOrWhiteSpace(conditionValues[i]))
                        {
                            continue;
                        }
                        cField = conditionFields[i].ToString();
                        //cField = Ordinary.StringReplacing(cField);
                        sqlText += " AND " + conditionFields[i] + "=@" + cField;
                    }
                }
                #endregion SqlText
                #region SqlExecution

                SqlCommand objComm = new SqlCommand(sqlText, currConn, transaction);
                if (conditionFields != null && conditionValues != null && conditionFields.Length == conditionValues.Length)
                {
                    for (int j = 0; j < conditionFields.Length; j++)
                    {
                        if (string.IsNullOrWhiteSpace(conditionFields[j]) || string.IsNullOrWhiteSpace(conditionValues[j]))
                        {
                            continue;
                        }
                        cField = conditionFields[j].ToString();
                        //cField = Ordinary.StringReplacing(cField);
                        objComm.Parameters.AddWithValue("@" + cField, conditionValues[j]);
                    }
                }

                if (Id !=null)
                {
                    objComm.Parameters.AddWithValue("@CustomerID", Id);
                }
                SqlDataReader dr;
                dr = objComm.ExecuteReader();
                while (dr.Read())
                {
                    vm = new CustomerVM();
                    vm.CustomerID = dr["CustomerID"].ToString();

                    vm.CustomerCode = dr["CustomerCode"].ToString();
                    vm.CustomerName = dr["CustomerName"].ToString();
                    vm.CustomerGroupID = dr["CustomerGroupID"].ToString();
                    vm.Address1 = dr["Address1"].ToString();
                    vm.Address2 = dr["Address2"].ToString();
                    vm.Address3 = dr["Address3"].ToString();
                    vm.City = dr["City"].ToString();
                    vm.TelephoneNo = dr["TelephoneNo"].ToString();
                    vm.FaxNo = dr["FaxNo"].ToString();
                    vm.Email = dr["Email"].ToString();
                    vm.StartDateTime = dr["StartDateTime"].ToString();
                    vm.ContactPerson = dr["ContactPerson"].ToString();
                    vm.ContactPersonDesignation = dr["ContactPersonDesignation"].ToString();
                    vm.ContactPersonTelephone = dr["ContactPersonTelephone"].ToString();
                    vm.ContactPersonEmail = dr["ContactPersonEmail"].ToString();
                    vm.TINNo = dr["TINNo"].ToString();
                    vm.VATRegistrationNo = dr["VATRegistrationNo"].ToString();
                    vm.Comments = dr["Comments"].ToString();
                    vm.ActiveStatus = dr["ActiveStatus"].ToString() == "Y" ? true : false;
                    vm.CreatedBy = dr["CreatedBy"].ToString();
                    vm.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                    vm.LastModifiedBy = dr["LastModifiedBy"].ToString();
                    vm.LastModifiedOn = DateTime.Parse(dr["LastModifiedOn"].ToString());
                    vm.Country = dr["Country"].ToString();
                    vm.VDSPercent = Convert.ToDecimal(dr["VDSPercent"]);
                    vm.BusinessType = dr["BusinessType"].ToString();
                    vm.BusinessCode = dr["BusinessCode"].ToString();

                    VMs.Add(vm);
                }
                dr.Close();

                #endregion SqlExecution

                if (Vtransaction == null && transaction != null)
                {
                    transaction.Commit();
                }
                #endregion
            }
            #region catch
            catch (SqlException sqlex)
            {
                throw new ArgumentNullException("", "SQL:" + sqlText + FieldDelimeter + sqlex.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("", "SQL:" + sqlText + FieldDelimeter + ex.Message.ToString());
            }
            #endregion
            #region finally
            finally
            {
                if (VcurrConn == null && currConn != null && currConn.State == ConnectionState.Open)
                {
                    currConn.Close();
                }
            }
            #endregion
            return VMs;
        }
        //==================Insert =================
        public string[] Insert(CustomerVM vm, SqlConnection VcurrConn = null, SqlTransaction Vtransaction = null)
        {
            #region Initializ
            string sqlText = "";
            int Id = 0;
            string[] retResults = new string[6];
            retResults[0] = "Fail";//Success or Fail
            retResults[1] = "Fail";// Success or Fail Message
            retResults[2] = Id.ToString();// Return Id
            retResults[3] = sqlText; //  SQL Query
            retResults[4] = "ex"; //catch ex
            retResults[5] = "InsertCustomer"; //Method Name
            SqlConnection currConn = null;
            SqlTransaction transaction = null;
            int transResult = 0;
            #endregion
            #region Try
            try
            {
                #region Validation
                #endregion Validation
                #region open connection and transaction
                #region New open connection and transaction
                if (VcurrConn != null)
                {
                    currConn = VcurrConn;
                }
                if (Vtransaction != null)
                {
                    transaction = Vtransaction;
                }
                #endregion New open connection and transaction
                if (currConn == null)
                {
                    currConn = _dbsqlConnection.GetConnection();
                    if (currConn.State != ConnectionState.Open)
                    {
                        currConn.Open();
                    }
                }
                if (transaction == null)
                {
                    transaction = currConn.BeginTransaction("");
                }
                #endregion open connection and transaction
                #region Save
                //vm.CustomerID = "14";

                if (vm != null)
                {
                    #region SqlText
                    sqlText = "  ";
                    sqlText += @" 
INSERT INTO Customers(
 CustomerID
,CustomerCode
,CustomerName
,CustomerGroupID
,Address1
,Address2
,Address3
,City
,TelephoneNo
,FaxNo
,Email
,StartDateTime
,ContactPerson
,ContactPersonDesignation
,ContactPersonTelephone
,ContactPersonEmail
,TINNo
,VATRegistrationNo
,Comments
,ActiveStatus
,CreatedBy
,CreatedOn
,LastModifiedBy
,LastModifiedOn
,Info2
,Info3
,Info4
,Info5
,Country
,VDSPercent
,BusinessType
,BusinessCode
) VALUES (
 @CustomerID
,@CustomerCode
,@CustomerName
,@CustomerGroupID
,@Address1
,@Address2
,@Address3
,@City
,@TelephoneNo
,@FaxNo
,@Email
,@StartDateTime
,@ContactPerson
,@ContactPersonDesignation
,@ContactPersonTelephone
,@ContactPersonEmail
,@TINNo
,@VATRegistrationNo
,@Comments
,@ActiveStatus
,@CreatedBy
,@CreatedOn
,@LastModifiedBy
,@LastModifiedOn
,@Info2
,@Info3
,@Info4
,@Info5
,@Country
,@VDSPercent
,@BusinessType
,@BusinessCode     
) 
";

                    #endregion SqlText
                    #region SqlExecution
                    SqlCommand cmdInsert = new SqlCommand(sqlText, currConn, transaction);

                    cmdInsert.Parameters.AddWithValue("@CustomerID", vm.CustomerID);

                    cmdInsert.Parameters.AddWithValue("@CustomerCode", vm.CustomerCode);
                    cmdInsert.Parameters.AddWithValue("@CustomerName", vm.CustomerName ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@CustomerGroupID", vm.CustomerGroupID);
                    cmdInsert.Parameters.AddWithValue("@Address1", vm.Address1 ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@Address2", vm.Address2 ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@Address3", vm.Address3 ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@City", vm.City ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@TelephoneNo", vm.TelephoneNo ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@FaxNo", vm.FaxNo ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@Email", vm.Email ?? Convert.DBNull);

                    cmdInsert.Parameters.AddWithValue("@StartDateTime",DateTime.Now.ToString());
                    cmdInsert.Parameters.AddWithValue("@ContactPerson", vm.ContactPerson ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@ContactPersonDesignation", vm.ContactPersonDesignation ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@ContactPersonTelephone", vm.ContactPersonTelephone ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@ContactPersonEmail", vm.ContactPersonEmail ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@VATRegistrationNo", vm.VATRegistrationNo ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@TINNo", vm.TINNo ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@Comments", vm.Comments ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@ActiveStatus", vm.ActiveStatus ? "Y" : "N");
                    cmdInsert.Parameters.AddWithValue("@CreatedBy", vm.CreatedBy);
                    cmdInsert.Parameters.AddWithValue("@CreatedOn", vm.CreatedOn);
                    cmdInsert.Parameters.AddWithValue("@LastModifiedBy", vm.LastModifiedBy ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@LastModifiedOn", vm.LastModifiedOn);
                    cmdInsert.Parameters.AddWithValue("@Country", vm.Country ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@Info2", vm.Info2 ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@Info3", vm.Info3 ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@Info4", vm.Info4 ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@Info5", vm.Info5 ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@VDSPercent", vm.VDSPercent);
                    cmdInsert.Parameters.AddWithValue("@BusinessType", vm.BusinessType ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@BusinessCode", vm.BusinessCode ?? Convert.DBNull);           

                    var exeRes = cmdInsert.ExecuteNonQuery();
                    transResult = Convert.ToInt32(exeRes);
                    if (transResult <= 0)
                    {
                        retResults[3] = sqlText;
                        throw new ArgumentNullException("Unexpected error to update Customer.", "");
                    }
                    #endregion SqlExecution
                }
                else
                {
                    retResults[1] = "This Customer already used!";
                    throw new ArgumentNullException("Please Input Customer Value", "");
                }
                #endregion Save
                #region Commit
                if (Vtransaction == null)
                {
                    if (transaction != null)
                    {
                        transaction.Commit();
                    }
                }
                #endregion Commit
                #region SuccessResult
                retResults[0] = "Success";
                retResults[1] = "Data Save Successfully.";
                retResults[2] = vm.CustomerID.ToString();
                #endregion SuccessResult
            }
            #endregion try
            #region Catch and Finall
            catch (Exception ex)
            {
                retResults[0] = "Fail";//Success or Fail
                retResults[4] = ex.Message.ToString(); //catch ex
                if (Vtransaction == null) { transaction.Rollback(); }
                return retResults;
            }
            finally
            {
                if (VcurrConn == null)
                {
                    if (currConn != null)
                    {
                        if (currConn.State == ConnectionState.Open)
                        {
                            currConn.Close();
                        }
                    }
                }
            }
            #endregion
            #region Results
            return retResults;
            #endregion
        }
        //==================Update =================
        public string[] Update(CustomerVM vm, SqlConnection VcurrConn = null, SqlTransaction Vtransaction = null)
        {
            #region Variables
            string[] retResults = new string[6];
            retResults[0] = "Fail";//Success or Fail
            retResults[1] = "Fail";// Success or Fail Message
            retResults[2] = "0";
            retResults[3] = "sqlText"; //  SQL Query
            retResults[4] = "ex"; //catch ex
            retResults[5] = "CustomerUpdate"; //Method Name
            int transResult = 0;
            string sqlText = "";
            SqlConnection currConn = null;
            SqlTransaction transaction = null;
            #endregion
            try
            {
                #region open connection and transaction
                #region New open connection and transaction
                if (VcurrConn != null)
                {
                    currConn = VcurrConn;
                }
                if (Vtransaction != null)
                {
                    transaction = Vtransaction;
                }
                #endregion New open connection and transaction
                if (currConn == null)
                {
                    currConn = _dbsqlConnection.GetConnection();
                    if (currConn.State != ConnectionState.Open)
                    {
                        currConn.Open();
                    }
                }
                if (transaction == null) { transaction = currConn.BeginTransaction("Update"); }
                #endregion open connection and transaction

                if (vm != null)
                {
                    #region Update Settings
                    #region SqlText
                    sqlText = "";
                    sqlText = "UPDATE Customers SET ";
                    sqlText += "  CustomerCode=@CustomerCode";
                    sqlText += " ,CustomerName=@CustomerName";
                    sqlText += " ,CustomerGroupID=@CustomerGroupID";
                    sqlText += " ,Address1=@Address1";
                    sqlText += " ,Address2=@Address2";
                    sqlText += " ,Address3=@Address3";
                    sqlText += " ,City=@City";
                    sqlText += " ,TelephoneNo=@TelephoneNo";
                    sqlText += " ,FaxNo=@FaxNo";
                    sqlText += " ,Email=@Email";
                    sqlText += " ,StartDateTime=@StartDateTime";
                    sqlText += " ,ContactPerson=@ContactPerson";
                    sqlText += " ,ContactPersonDesignation=@ContactPersonDesignation";
                    sqlText += " ,ContactPersonTelephone=@ContactPersonTelephone";
                    sqlText += " ,ContactPersonEmail=@ContactPersonEmail";
                    sqlText += " ,VATRegistrationNo=@VATRegistrationNo";
                    sqlText += " ,TINNo=@TINNo";
                    sqlText += " ,Comments=@Comments";
                    sqlText += " ,LastModifiedBy=@LastModifiedBy";
                    sqlText += " ,LastModifiedOn=@LastModifiedOn";
                    sqlText += " ,Country=@Country";
                    sqlText += " ,Info2=@Info2";
                    sqlText += " ,Info3=@Info3";
                    sqlText += " ,Info4=@Info4";
                    sqlText += " ,Info5=@Info5";
                    sqlText += " ,VDSPercent=@VDSPercent";
                    sqlText += " ,BusinessType=@BusinessType";
                    sqlText += " ,BusinessCode=@BusinessCode";

                    sqlText += " WHERE CustomerID=@CustomerID";

                    #endregion SqlText
                    #region SqlExecution
                    SqlCommand cmdUpdate = new SqlCommand(sqlText, currConn, transaction);
                    cmdUpdate.Parameters.AddWithValue("@CustomerID", vm.CustomerID);
                    cmdUpdate.Parameters.AddWithValue("@CustomerCode", vm.CustomerCode);
                    cmdUpdate.Parameters.AddWithValue("@CustomerName", vm.CustomerName ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@CustomerGroupID", vm.CustomerGroupID);
                    cmdUpdate.Parameters.AddWithValue("@Address1", vm.Address1 ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@Address2", vm.Address2 ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@Address3", vm.Address3 ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@City", vm.City ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@TelephoneNo", vm.TelephoneNo ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@FaxNo", vm.FaxNo ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@Email", vm.Email ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@StartDateTime", DateTime.Now.ToString());
                    cmdUpdate.Parameters.AddWithValue("@ContactPerson", vm.ContactPerson ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@ContactPersonDesignation", vm.ContactPersonDesignation ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@ContactPersonTelephone", vm.ContactPersonTelephone ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@ContactPersonEmail", vm.ContactPersonEmail ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@VATRegistrationNo", vm.VATRegistrationNo ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@TINNo", vm.TINNo ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@Comments", vm.Comments ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@LastModifiedBy", vm.LastModifiedBy ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@LastModifiedOn", vm.LastModifiedOn);
                    cmdUpdate.Parameters.AddWithValue("@Country", vm.Country ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@Info2", vm.Info2 ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@Info3", vm.Info3 ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@Info4", vm.Info4 ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@Info5", vm.Info5 ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@VDSPercent", vm.VDSPercent);
                    cmdUpdate.Parameters.AddWithValue("@BusinessType", vm.BusinessType ?? Convert.DBNull);
                    cmdUpdate.Parameters.AddWithValue("@BusinessCode", vm.BusinessCode ?? Convert.DBNull); 
    
 
                    var exeRes = cmdUpdate.ExecuteNonQuery();
                    transResult = Convert.ToInt32(exeRes);
                    if (transResult <= 0)
                    {
                        retResults[3] = sqlText;
                        throw new ArgumentNullException("Unexpected error to update Customer.", "");
                    }
                    #endregion SqlExecution

                    retResults[2] = vm.CustomerID.ToString();// Return Id
                    retResults[3] = sqlText; //  SQL Query
                    #region Commit
                    if (transResult <= 0)
                    {
                        // throw new ArgumentNullException("Customer Update", vm.BranchId + " could not updated.");
                    }
                    #endregion Commit
                    #endregion Update Settings
                }
                else
                {
                    throw new ArgumentNullException("Customer Update", "Could not found any item.");
                }
                #region Commit
                if (Vtransaction == null)
                {
                    if (transaction != null)
                    {
                        transaction.Commit();
                    }
                }
                #endregion Commit
                #region SuccessResult
                retResults[0] = "Success";
                retResults[1] = "Data Save Successfully.";
                retResults[2] = vm.CustomerID.ToString();
                #endregion SuccessResult
            }
            #region catch
            catch (Exception ex)
            {
                retResults[0] = "Fail";//Success or Fail
                retResults[4] = ex.Message; //catch ex
                if (Vtransaction == null) { transaction.Rollback(); }
                return retResults;
            }
            finally
            {
                if (VcurrConn == null && currConn != null && currConn.State == ConnectionState.Open)
                {
                    currConn.Close();
                }
            }
            #endregion
            return retResults;
        }
        ////==================Delete =================
        public string[] Delete(string[] ids, SqlConnection VcurrConn = null, SqlTransaction Vtransaction = null)
        {
            #region Variables
            string[] retResults = new string[6];
            retResults[0] = "Fail";//Success or Fail
            retResults[1] = "Fail";// Success or Fail Message
            retResults[2] = "0";// Return Id
            retResults[3] = "sqlText"; //  SQL Query
            retResults[4] = "ex"; //catch ex
            retResults[5] = "DeleteCustomer"; //Method Name
            int transResult = 0;
            string sqlText = "";
            SqlConnection currConn = null;
            SqlTransaction transaction = null;
            //string retVal = "";
            #endregion
            try
            {
                #region open connection and transaction
                #region New open connection and transaction
                if (VcurrConn != null)
                {
                    currConn = VcurrConn;
                }
                if (Vtransaction != null)
                {
                    transaction = Vtransaction;
                }
                #endregion New open connection and transaction
                if (currConn == null)
                {
                    currConn = _dbsqlConnection.GetConnection();
                    if (currConn.State != ConnectionState.Open)
                    {
                        currConn.Open();
                    }
                }
                if (transaction == null) { transaction = currConn.BeginTransaction("Delete"); }
                #endregion open connection and transaction
                if (ids.Length >= 1)
                {

                    #region Update Settings
                    for (int i = 0; i < ids.Length - 1; i++)
                    {
                        sqlText = "";
                        sqlText = "update Customers set";
                        sqlText += " ActiveStatus=@ActiveStatus";
                        sqlText += " ,LastModifiedBy=@LastModifiedBy";
                        sqlText += " ,LastModifiedOn=@LastModifiedOn";
                        sqlText += " where CustomerID=@CustomerID";

                        SqlCommand cmdUpdate = new SqlCommand(sqlText, currConn, transaction);
                        cmdUpdate.Parameters.AddWithValue("@CustomerID", ids[i]);
                        cmdUpdate.Parameters.AddWithValue("@ActiveStatus", "N");
                        cmdUpdate.Parameters.AddWithValue("@LastModifiedBy", "Robin");
                        cmdUpdate.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToShortDateString());
                        var exeRes = cmdUpdate.ExecuteNonQuery();
                        transResult = Convert.ToInt32(exeRes);
                    }
                    retResults[2] = "";// Return Id
                    retResults[3] = sqlText; //  SQL Query
                    #region Commit
                    if (transResult <= 0)
                    {
                        throw new ArgumentNullException("Customer Delete could not Delete.");
                    }
                    #endregion Commit
                    #endregion Update Settings
                }
                else
                {
                    throw new ArgumentNullException("Customer Information Delete", "Could not found any item.");
                }
                if (Vtransaction == null && transaction != null)
                {
                    transaction.Commit();
                    retResults[0] = "Success";
                    retResults[1] = "Data Delete Successfully.";
                }
            }
            #region catch
            catch (Exception ex)
            {
                retResults[0] = "Fail";//Success or Fail
                retResults[4] = ex.Message; //catch ex
                if (Vtransaction == null) { transaction.Rollback(); }
                return retResults;
            }
            finally
            {
                if (VcurrConn == null && currConn != null && currConn.State == ConnectionState.Open)
                {
                    currConn.Close();
                }
            }
            #endregion
            return retResults;
        }

        #endregion
    }
}
