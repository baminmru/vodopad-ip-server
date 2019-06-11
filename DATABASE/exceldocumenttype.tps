CREATE OR REPLACE TYPE ExcelDocumentType AS OBJECT(

   -- This is constant for the new line character.
   NL_CHAR            CHAR(1),

   -- Worksheet Header/Footer Format String
   WHF_FORMAT_PAGE     VARCHAR2(6),
   WHF_FORMAT_PAGES    VARCHAR2(6),
   WHF_FORMAT_DATE     VARCHAR2(6),
   WHF_FORMAT_TIME     VARCHAR2(6),
   WHF_FORMAT_FILEPATH VARCHAR2(6),
   WHF_FORMAT_FILE     VARCHAR2(6),
   WHF_FORMAT_TAB      VARCHAR2(6),
   WHF_FORMAT_LEFT     VARCHAR2(6),
   WHF_FORMAT_RIGHT    VARCHAR2(6),
   WHF_FORMAT_CENTER   VARCHAR2(6),
   WHF_FORMAT_FONT     VARCHAR2(8),

    -- Column Width Multiplier
    CW_MULT           NUMBER(2,1),

     object_id        NUMBER(12),

    -- Index (counter) variables for each
    -- segment of the spreadsheet.
    -- All of the segments will be assembled
    -- into the completed document.

    styleSegIndex              NUMBER(12),
    colDefSegIndex             NUMBER(12),
    dataSegIndex               NUMBER(12),
    worksheetSegIndex          NUMBER(12),
    rowsSegIndex               NUMBER(12),
    cellsSegIndex              NUMBER(12),
    sheetHeaderFooterSegIndex  NUMBER(12),
    sheetHeaderDataIndex       NUMBER(12),
    sheetFooterDataIndex       NUMBER(12),


    documentIndex     NUMBER(12),

    -- Document information variables
    -- Row and Column count are required
    -- by Excel.
    -- Document Length is used when displaying
    -- the XML back via HTP or when generating
    -- a CLOB containing the document.

    row_count         NUMBER(12),
    temp_col_count    NUMBER(12),
    col_count         NUMBER(12),
    document_length   NUMBER(12),

    -- Constructor
    CONSTRUCTOR FUNCTION ExcelDocumentType RETURN SELF AS RESULT,

    -- Member Methods
    MEMBER PROCEDURE pushValue(p_index   NUMBER,
                               p_segment VARCHAR2,
                               p_value   VARCHAR2),
    MEMBER PROCEDURE purgeSegment(p_segment VARCHAR2 := NULL),
    MEMBER PROCEDURE documentOpen,
    MEMBER PROCEDURE documentClose,
    MEMBER PROCEDURE worksheetOpen(p_worksheetname VARCHAR2 := NULL),

    MEMBER PROCEDURE worksheetHeaderFooterOpen,
    MEMBER PROCEDURE worksheetHeaderValues(p_headerstring VARCHAR2 := NULL,
                                           p_fontsize     VARCHAR2 := NULL),
    MEMBER PROCEDURE worksheetFooterValues(p_footerstring VARCHAR2 := NULL,
                                           p_fontsize     VARCHAR2 := NULL),
    MEMBER PROCEDURE worksheetHeaderFooterClose,
    MEMBER PROCEDURE worksheetClose,

   MEMBER PROCEDURE rowOpen(p_style       VARCHAR2 := NULL,
                            p_custom_attr VARCHAR2 := NULL),
   MEMBER PROCEDURE rowClose,

   MEMBER PROCEDURE defineColumn(p_index VARCHAR2       := NULL,
                                 p_width NUMBER       := NULL,
                                 p_custom_attr VARCHAR2 := NULL),

   MEMBER PROCEDURE defaultStyle,

   MEMBER PROCEDURE stylesOpen,
   MEMBER PROCEDURE stylesClose,

   MEMBER PROCEDURE createStyle(p_style_id         VARCHAR2 := NULL,
                                p_font             VARCHAR2 := NULL,
                                p_ffamily          VARCHAR2 := NULL,
                                p_fsize            VARCHAR2 := NULL,
                                p_bold             VARCHAR2 := NULL,
                                p_italic           VARCHAR2 := NULL,
                                p_underline        VARCHAR2 := NULL,
                                p_text_color       VARCHAR2 := NULL,
                                p_cell_color       VARCHAR2 := NULL,
                                p_cell_pattern     VARCHAR2 := NULL,
                                p_align_vertical   VARCHAR2 := NULL,
                                p_align_horizontal VARCHAR2 := NULL,
                                p_wrap_text        VARCHAR2 := NULL,
                                p_number_format    VARCHAR2 := NULL,
                                p_custom_xml       VARCHAR2 := NULL),


   MEMBER PROCEDURE addCell(p_col_index   VARCHAR2 := NULL,
                            p_data        VARCHAR2 := NULL,
                            p_data_type   VARCHAR2 := 'String',
                            p_style       VARCHAR2 := NULL,
                            p_formula     VARCHAR2 := NULL,
                            p_custom_attr VARCHAR2 := NULL),

   MEMBER PROCEDURE displayDocument,
   MEMBER FUNCTION getDocument           RETURN CLOB,
   MEMBER FUNCTION getDocumentData       RETURN ExcelDocumentLine

);
/

