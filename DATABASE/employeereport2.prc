CREATE OR REPLACE PROCEDURE employeeReport2 AS

-- Test statements here
   v_sql_salary        VARCHAR2(200) := 'SELECT last_name,first_name,salary FROM hr.employees ORDER BY last_name,first_name';
   v_sql_contact       VARCHAR2(200) := 'SELECT last_name,first_name,phone_number,email FROM hr.employees ORDER BY last_name,first_name';
   v_sql_hiredate      VARCHAR2(200) := 'SELECT last_name,first_name,to_char(hire_date,"MM/DD/YYYY") hire_date FROM hr.employees ORDER BY last_name,first_name';
   excelReport         ExcelDocumentType := ExcelDocumentType();
   v_worksheet_rec     ExcelDocTypeUtils.T_WORKSHEET_DATA := NULL;
--   v_worksheet_array   ExcelDocTypeUtils.WORKSHEET_TABLE  := ExcelDocTypeUtils.WORKSHEET_TABLE();
   v_worksheet_array   ExcelDocTypeUtils.WORKSHEET_TABLE := ExcelDocTypeUtils.WORKSHEET_TABLE();


BEGIN

   -- Salary
   v_worksheet_rec.query           := v_sql_salary;
   v_worksheet_rec.worksheet_name  := 'Salaries';
   v_worksheet_rec.col_count       := 3;
   v_worksheet_rec.col_width_list  := '25,20,15';
   v_worksheet_rec.col_header_list := 'Lastname,Firstname,Salary';

   v_worksheet_array.EXTEND;
   v_worksheet_array(v_worksheet_array.count) := v_worksheet_rec;

   -- Contact
   v_worksheet_rec.query           := v_sql_contact;
   v_worksheet_rec.worksheet_name  := 'Contact_Info';
   v_worksheet_rec.col_count       := 4;
   v_worksheet_rec.col_width_list  := '25,20,20,25';
   v_worksheet_rec.col_header_list := 'Lastname,Firstname,Phone,Email';

   v_worksheet_array.EXTEND;
   v_worksheet_array(v_worksheet_array.count) := v_worksheet_rec;

   -- Contact
   v_worksheet_rec.query           := v_sql_hiredate;
   v_worksheet_rec.worksheet_name  := 'Hiredate';
   v_worksheet_rec.col_count       := 3;
   v_worksheet_rec.col_width_list  := '25,20,20';
   v_worksheet_rec.col_header_list := 'Lastname,Firstname,Hiredate';

   v_worksheet_array.EXTEND;
   v_worksheet_array(v_worksheet_array.count) := v_worksheet_rec;

-- Çהוסü מרטבךא!
--   excelReport := ExcelDocTypeUtils.createExcelDocument(v_worksheet_array);

   excelReport.displayDocument;

END;
/

