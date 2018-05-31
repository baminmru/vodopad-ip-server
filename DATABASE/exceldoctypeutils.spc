CREATE OR REPLACE PACKAGE ExcelDocTypeUtils AS

   TYPE t_refcursor IS REF CURSOR;

   pv_result_table  RESULT_TABLE := RESULT_TABLE();


   /* This type allows the user to create a title row at the top of a worksheet */

   TYPE T_SHEET_TITLE IS RECORD(
       title      VARCHAR2(1000),
       cell_span  NUMBER(12),
       style      VARCHAR2(200)
   );

   /* This record contains all of the components required to create an Excel Report worksheet. */
   TYPE T_WORKSHEET_DATA IS RECORD(
       query             VARCHAR2(4000),
       title             T_SHEET_TITLE,
       worksheet_name    VARCHAR2(50),
       col_count         NUMBER(3),
       col_width_list    VARCHAR2(500),
       col_caption       VARCHAR2(2000),
       col_header_list   VARCHAR2(2000),
       col_datatype_list VARCHAR2(4000),
       col_style_list    VARCHAR2(5000),
       col_formula_list  VARCHAR2(4000)
   );


   /* An Array of T_WORKSHEET_DATA allows us to create an excel document with multiple worksheets based on
      different queries. */
   TYPE WORKSHEET_TABLE IS TABLE OF T_WORKSHEET_DATA;


   /* This record structure matches the createStyle method of the ExcelDocumentType. */
   TYPE T_STYLE_DEF IS RECORD(
                                p_style_id         VARCHAR2(50),
                                p_font             VARCHAR2(50),
                                p_ffamily          VARCHAR2(50),
                                p_fsize            VARCHAR2(50),
                                p_bold             VARCHAR2(1),
                                p_italic           VARCHAR2(1),
                                p_underline        VARCHAR2(1),
                                p_text_color       VARCHAR2(50),
                                p_cell_color       VARCHAR2(50),
                                p_cell_pattern     VARCHAR2(50),
                                p_align_vertical   VARCHAR2(50),
                                p_align_horizontal VARCHAR2(50),
                                p_wrap_text        VARCHAR2(1),
                                p_number_format    VARCHAR2(100),
                                p_custom_xml       VARCHAR2(4000)
                             );


   /* Collection of styles that can applied to cells */
   TYPE STYLE_LIST IS TABLE OF T_STYLE_DEF;


   FUNCTION createExcelDocument(p_worksheet_data WORKSHEET_TABLE,
                                p_style_data     STYLE_LIST := STYLE_LIST()) RETURN ExcelDocumentType;


END;
/

