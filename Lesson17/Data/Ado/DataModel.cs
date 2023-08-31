using System.Data;

namespace Lesson17.Data.Ado;

public class CustomerAdapter {

}

public class CustomerTableModel {
	private const string TABLE_NAME = "Customers";
	private readonly DataSet _dataSet;
    private DataTable? _table { get => _dataSet.Tables[TABLE_NAME]; }

	public CustomerTableModel(DataSet dataSet) {
		_dataSet = dataSet;
	}

    //public List<CustomerModel> Rows {
    //    get {
    //        var result = new List<CustomerModel>();
    //        foreach(DataRow row in _table.Rows) {
    //            var model = new CustomerModel();

    //            model.SetId(row[0]);
    //        }
    //    }
    //}

	public class CustomerModel {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }


        public void SetId(object value) {
            if (value == null) {
                //this.Id = DBNull.Value;
            }
        }
    }
}
