CREATE OR REPLACE PACKAGE ORA_EXCEL IS
   /*
    *  ORAEXCEL version 3.1.2
    *  Documentation and examples available on http://www.oraexcel.com/documentation
    *
    */
    license_key VARCHAR2(100):= '2402328834198812';

    /***************************************************************************
    *
    * ORA_EXCEL COPYRIGHT AND LEGAL NOTES
    *
    * This software is protected by International copyright Law. Unauthorized use,
    * duplication, reverse engineering, any form of redistribution, or use in part
    * or in whole other than by prior, express, printed and signed license for use
    * is subject to civil and criminal prosecution. If you have received this file
    * in error, please notify copyright holder and destroy this and any other copies
    * as instructed.
    *
    * END-USER LICENSE AGREEMENT FOR ORA_EXCEL IMPORTANT PLEASE READ THE TERMS AND
    * CONDITIONS OF THIS LICENSE AGREEMENT CAREFULLY BEFORE CONTINUING WITH THIS
    * PROGRAM INSTALL: ORA_EXCEL End-User License Agreement ("EULA") is a legal
    * agreement between you (either an individual or a single entity) and ORA_EXCEL.
    * For the ORA_EXCEL software product(s) identified above which may include
    * associated software components, media, printed materials, and "online" or
    * electronic documentation ("ORA_EXCEL"). By installing, copying, or otherwise
    * using the SOFTWARE PRODUCT, you agree to be bound by the terms of this EULA.
    * This license agreement represents the entire agreement concerning the program
    * between you and ORA_EXCEL, (referred to as "licenser"), and it supersedes any
    * prior proposal, representation, or understanding between the parties. If you
    * do not agree to the terms of this EULA, do not install or use the SOFTWARE PRODUCT.
    *
    * The SOFTWARE PRODUCT is protected by copyright laws and international copyright
    * treaties, as well as other intellectual property laws and treaties.
    * The SOFTWARE PRODUCT is licensed, not sold.
    *
    * 1. GRANT OF LICENSE.
    * The SOFTWARE PRODUCT is licensed as follows:
    * (a) Installation and Use.
    * ORA_EXCEL grants you the right to install and use copies of the SOFTWARE PRODUCT
    * on your computer running a validly licensed copy of the operating system for which
    * the SOFTWARE PRODUCT was designed.
    * (b) Backup Copies.
    * You may also make copies of the SOFTWARE PRODUCT as may be necessary for backup
    * and archival purposes.
    *
    * 2. DESCRIPTION OF OTHER RIGHTS AND LIMITATIONS.
    * (a) Maintenance of Copyright Notices.
    * You must not remove or alter any copyright notices on any and all copies of the
    * SOFTWARE PRODUCT.
    * (b) Distribution.
    * You may not distribute registered copies of the SOFTWARE PRODUCT to third parties.
    * Evaluation versions available for download from ORA_EXCEL's websites may be freely
    * distributed.
    * (c) Prohibition on Reverse Engineering, Decompilation, and Disassembly.
    * You may not reverse engineer, decompile, or disassemble the SOFTWARE PRODUCT,
    * except and only to the extent that such activity is expressly permitted by
    * applicable law notwithstanding this limitation.
    * (d) Rental.
    * You may not rent, lease, or lend the SOFTWARE PRODUCT.
    * (e) Support Services.
    * ORA_EXCEL may provide you with support services related to the SOFTWARE PRODUCT
    * ("Support Services"). Any supplemental software code provided to you as part of
    * the Support Services shall be considered part of the SOFTWARE PRODUCT and subject
    * to the terms and conditions of this EULA.
    * (f) Compliance with Applicable Laws.
    * You must comply with all applicable laws regarding use of the SOFTWARE PRODUCT.
    * 3. TERMINATION
    * Without prejudice to any other rights, ORA_EXCEL may terminate this EULA if you
    * fail to comply with the terms and conditions of this EULA. In such event, you must
    * destroy all copies of the SOFTWARE PRODUCT in your possession.
    * 4. COPYRIGHT
    * All title, including but not limited to copyrights, in and to the SOFTWARE PRODUCT
    * and any copies thereof are owned by ORA_EXCEL or its suppliers. All title and
    * intellectual property rights in and to the content which may be accessed through
    * use of the SOFTWARE PRODUCT is the property of the respective content owner and
    * may be protected by applicable copyright or other intellectual property laws and
    * treaties. This EULA grants you no rights to use such content. All rights not
    * expressly granted are reserved by ORA_EXCEL.
    * 5. NO WARRANTIES
    * ORA_EXCEL expressly disclaims any warranty for the SOFTWARE PRODUCT.
    * The SOFTWARE PRODUCT is provided 'As Is' without any express or implied warranty
    * of any kind, including but not limited to any warranties of merchantability,
    * noninfringement, or fitness of a particular purpose. ORA_EXCEL does not warrant
    * or assume responsibility for the accuracy or completeness of any information,
    * text, graphics, links or other items contained within the SOFTWARE PRODUCT.
    * ORA_EXCEL makes no warranties respecting any harm that may be caused by the
    * transmission of a computer virus, worm, time bomb, logic bomb, or other such
    * computer program. ORA_EXCEL further expressly disclaims any warranty or
    * representation to Authorized Users or to any third party.
    * 6. LIMITATION OF LIABILITY
    * In no event shall ORA_EXCEL be liable for any damages (including, without
    * limitation, lost profits, business interruption, or lost information) rising
    * out of 'Authorized Users' use of or inability to use the SOFTWARE PRODUCT,
    * even if ORA_EXCEL has been advised of the possibility of such damages.
    * In no event will ORA_EXCEL be liable for loss of data or for indirect,
    * special, incidental, consequential (including lost profit), or other
    * damages based in contract, tort or otherwise. ORA_EXCEL shall have no
    * liability with respect to the content of the SOFTWARE PRODUCT or any part
    * thereof, including but not limited to errors or omissions contained therein,
    * libel, infringements of rights of publicity, privacy, trademark rights,
    * business interruption, personal injury, loss of privacy, moral rights or
    * the disclosure of confidential information.
    *
    ****************************************************************************/



    TYPE "@1" IS RECORD ("@2" VARCHAR2(10),
                                           "@3" NUMBER,
                                           "@4" INTEGER DEFAULT 0);
    TYPE "@5" IS TABLE OF "@1";
    "@6" "@5" := "@5"();


    TYPE "@7" IS RECORD("@8" VARCHAR2(100));
    TYPE "@9" IS TABLE OF "@7";
    "@10" "@9" := "@9"();


    TYPE "@11" IS RECORD ("@12" VARCHAR2(1000),
                                    "@13" VARCHAR2(10));
    TYPE "@14" IS table OF "@11";
    "@26" "@14" := "@14"();

    TYPE "@728" IS RECORD ("@12" VARCHAR2(1000),
                                             "@13" VARCHAR2(10));
    TYPE "@729" IS table OF "@728";
    "@730" "@729" := "@729"();



    TYPE "@15" IS RECORD ("@16" VARCHAR2(1000),
                                  "@17" VARCHAR2(30),
                                  "@18" NUMBER,
                                  "@19" NUMBER,
                                  "@20" INTEGER,
                                  "@21" INTEGER,
                                  "@22" VARCHAR2(10));
    TYPE "@23" IS TABLE OF "@15";
    "@27" "@23" := "@23"();

    TYPE "@24" IS TABLE OF CLOB;
    "@25" "@24" := "@24"();


    TYPE "@28" IS RECORD ("@29" VARCHAR2(20),
                                      "@30" VARCHAR2(1),
                                      "@31" VARCHAR2(100),
                                      "@32" PLS_INTEGER := 0,
                                      "@33" PLS_INTEGER := 0,
                                      "@34" PLS_INTEGER := 0,
                                      "@35" PLS_INTEGER := 0,
                                      "@36" PLS_INTEGER := 0,
                                      "@37" PLS_INTEGER,
                                      "@38" VARCHAR2(4000),
                                      "@39" BOOLEAN := FALSE,
                                      "@40" BOOLEAN := FALSE,
                                      "@41" BOOLEAN := FALSE,
                                      "@42" VARCHAR2(8),
                                      "@43" VARCHAR2(8),
                                      "@44" VARCHAR2(1),
                                      "@45" VARCHAR2(1),
                                      "@46" BOOLEAN DEFAULT FALSE,
                                      "@47" BOOLEAN DEFAULT FALSE,
                                      "@48" VARCHAR2(6),
                                      "@49" VARCHAR2(8),
                                      "@50" BOOLEAN DEFAULT FALSE,
                                      "@51" VARCHAR2(6),
                                      "@52" VARCHAR2(8),
                                      "@53" BOOLEAN DEFAULT FALSE,
                                      "@54" VARCHAR2(6),
                                      "@55" VARCHAR2(8),
                                      "@56" BOOLEAN DEFAULT FALSE,
                                      "@57" VARCHAR2(6),
                                      "@58" VARCHAR2(8),
                                      "@59" VARCHAR2(10),
                                      "@60" VARCHAR2(8),
                                      "@61" BOOLEAN DEFAULT FALSE,
                                      "@62" BOOLEAN,
                                      "@63" VARCHAR2(10),
                                      "@64" VARCHAR2(10),
                                      "@65" PLS_INTEGER,
                                      "@66" BOOLEAN,
                                      "@67" PLS_INTEGER := 0,
                                      "@68" INTEGER := 0,
                                      "@69" INTEGER := 0,
                                      "@70" INTEGER := 0,
                                      "@370" CLOB);
    "@71" "@28";
    TYPE "@72" IS TABLE OF "@28" INDEX BY PLS_INTEGER;

    "@73" "@72";

    TYPE "@444" IS RECORD ("@445" NUMBER,
                                              "@446" NUMBER,
                                              "@447" NUMBER);
    TYPE "@450" IS TABLE OF "@444";
    "@449" "@450" := "@450"();


    TYPE "@74" IS RECORD ("@75" VARCHAR2(32),
                                "@76" CLOB,
                                "@77" "@24",
                                "@78" PLS_INTEGER,
                                "@79" PLS_INTEGER,
                                "@180" "@5",
                                "@181" "@9",
                                "@80" BOOLEAN DEFAULT FALSE,
                                "@81" NUMBER,
                                "@82" NUMBER,
                                "@83" NUMBER,
                                "@84" NUMBER,
                                "@85" NUMBER,
                                "@86" NUMBER,
                                "@87" VARCHAR2(10),
                                "@88" INTEGER,
                                "@89" VARCHAR2(1000),
                                "@92" VARCHAR2(1000),
                                "@182" "@14",
                                "@731" "@729",
                                "@183" "@23",
                                "@90" VARCHAR2(20),
                                "@91" VARCHAR2(20),
                                "@93" "@72",
                                "@94" NUMBER,
                                "@95" BOOLEAN,
                                "@892" VARCHAR2(20),
                                "@893" VARCHAR2(20),
                                "@442" NUMBER,
                                "@443" NUMBER,
                                "@448" "@450",
                                "@573" NUMBER DEFAULT 0,
                                "@574" CLOB,
                                "@575" VARCHAR2(32767),
                                "@798" VARCHAR2(20));

    TYPE "@96" IS TABLE OF "@74";
    "@97" "@96" := "@96"();



    TYPE "@98" IS RECORD("@184" VARCHAR(60));
    TYPE "@99" IS TABLE OF "@98";
    "@100" "@99" := "@99"();


    TYPE "@101" IS RECORD ("@102" PLS_INTEGER,
                                "@103" PLS_INTEGER,
                                "@104" PLS_INTEGER,
                                "@105" PLS_INTEGER,
                                "@106" PLS_INTEGER,
                                "@107" VARCHAR2(1),
                                "@108" VARCHAR2(1),
                                "@109" BOOLEAN,
                                "@110" INTEGER,
                                "@111" INTEGER,
                                "@112" INTEGER);
    TYPE "@113" IS table OF "@101";
    "@114" "@113" := "@113"();




    TYPE "@115" IS RECORD ("@116" PLS_INTEGER,
                               "@117" VARCHAR2(100),
                               "@118" BOOLEAN := FALSE,
                               "@119" BOOLEAN := FALSE,
                               "@120" BOOLEAN := FALSE,
                               "@121" VARCHAR2(8));
    TYPE "@122" IS table OF "@115";
    "@123" "@122" := "@122"();


    TYPE "@124" IS RECORD ("@125" VARCHAR2(10),
                               "@126" VARCHAR2(8));
    TYPE "@127" IS TABLE OF "@124";
    "@128" "@127" := "@127"();



    TYPE "@129" IS RECORD ("@130" PLS_INTEGER,
                                      "@131" VARCHAR2(100));


    TYPE "@132" IS RECORD ("@133" BOOLEAN DEFAULT FALSE,
                                 "@134" VARCHAR2(6),
                                 "@135" VARCHAR2(8),
                                 "@136" BOOLEAN DEFAULT FALSE,
                                 "@137" VARCHAR2(6),
                                 "@138" VARCHAR2(8),
                                 "@139" BOOLEAN DEFAULT FALSE,
                                 "@140" VARCHAR2(6),
                                 "@141" VARCHAR2(8),
                                 "@142" BOOLEAN DEFAULT FALSE,
                                 "@143" VARCHAR2(6),
                                 "@144" VARCHAR2(8),
                                 "@145" VARCHAR2(10),
                                 "@146" VARCHAR2(8));
    TYPE "@147" IS TABLE OF "@132";
    "@148" "@147" := "@147"();


    TYPE "@149" IS TABLE OF CLOB;
    "@150" "@149" := "@149"();

    TYPE "@753" IS RECORD ("@752" VARCHAR2(20),
                                "@754" VARCHAR2(100),
                                "@755" PLS_INTEGER,
                                "@38" VARCHAR2(4000),
                                "@39" BOOLEAN := FALSE,
                                "@40" BOOLEAN := FALSE,
                                "@41" BOOLEAN := FALSE,
                                "@42" VARCHAR2(8),
                                "@43" VARCHAR2(8),
                                "@44" VARCHAR2(20),
                                "@45" VARCHAR2(20),
                                "@47" BOOLEAN DEFAULT FALSE,
                                "@48" VARCHAR2(6),
                                "@49" VARCHAR2(8),
                                "@50" BOOLEAN DEFAULT FALSE,
                                "@51" VARCHAR2(6),
                                "@52" VARCHAR2(8),
                                "@53" BOOLEAN DEFAULT FALSE,
                                "@54" VARCHAR2(6),
                                "@55" VARCHAR2(8),
                                "@56" BOOLEAN DEFAULT FALSE,
                                "@57" VARCHAR2(6),
                                "@58" VARCHAR2(8),
                                "@756" BOOLEAN,
                                "@59" VARCHAR2(10),
                                "@60" VARCHAR2(8),
                                "@61" BOOLEAN DEFAULT FALSE,
                                "@67" VARCHAR2(60) := NULL,
                                "@68" INTEGER := 0,
                                "@69" INTEGER := 0,
                                "@70" INTEGER := 0,
                                "@764" INTEGER :=0);


    TYPE "@757" IS TABLE OF "@753" INDEX BY VARCHAR2(50);




    TYPE "@151" IS RECORD ("@152" "@96",
                                   "@153" CLOB,
                                   "@154" "@149",
                                   "@155" PLS_INTEGER,
                                   "@156" "@99",
                                   "@157" "@113",
                                   "@158" "@122",
                                   "@159" "@129",
                                   "@160" "@127",
                                   "@161" "@147",
                                   "@162" BOOLEAN,
                                   "@732" BOOLEAN,
                                   "@582" VARCHAR2(1000),
                                   "@758" "@757");
    TYPE "@163" IS TABLE OF "@151";
    "@164" "@163";
    "@165" "@163" := "@163"();


    doc_id PLS_INTEGER := 0;
    current_doc_id PLS_INTEGER;
    current_sheet_id PLS_INTEGER;
    current_row_id PLS_INTEGER;
    "@167" CLOB;
    "@168" PLS_INTEGER;
    "@169" BOOLEAN := FALSE;


    "@170" PLS_INTEGER;
    "@171" VARCHAR2(20);
    "@172" VARCHAR2(20);
    "@173" PLS_INTEGER;

    "@270" NUMBER := 4000;
    "@271" NUMBER := 32767;

    TYPE "@174" IS TABLE OF VARCHAR2(32767) INDEX BY PLS_INTEGER;
    "@175" "@174";

    TYPE "@176" IS TABLE OF VARCHAR2(32767) INDEX BY PLS_INTEGER;
    "@177" "@176";

    TYPE "@178" IS TABLE OF VARCHAR2(32767) INDEX BY PLS_INTEGER;
    "@179" "@178";

    "@300" BLOB := EMPTY_BLOB();
    "@301" CLOB := EMPTY_CLOB();
    "@302" CLOB := EMPTY_CLOB();

    TYPE cell_value_type IS RECORD(value VARCHAR2(32767),
                                   varchar2_value VARCHAR2(32767),
                                   number_value NUMBER,
                                   date_value DATE,
                                   type VARCHAR2(1));
    "@309" INTEGER;
    "@310" CLOB;
    TYPE "@311" IS TABLE OF VARCHAR2(1) INDEX BY PLS_INTEGER;
    "@312" "@311";
    "@313" "@311";
    "@314" VARCHAR2(4);
    "@315" VARCHAR2(30);
    "@452" INTEGER;

    /***************************************************************************
    * Description: Creates new instance of excel document, initializes space
    *  storage and prepares parameters
    *
    * Input Parameters:
    *   - no input parameters
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - doc_id [unique identifier of document]
    *
    ****************************************************************************/
    FUNCTION new_document
    RETURN PLS_INTEGER;



    /***************************************************************************
    * Description: Wrapper procedure of new_document function
    *
    * Input Parameters:
    *   - no input parameters
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE new_document;



    /***************************************************************************
    * Description: Adds new sheet to document
    *
    * Input Parameters:
    *   - sheet_name [sheet name that will be displayed on sheet tab]
    *   - doc_id     [unique identificator od document]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - sheet_id  [unique identificator of sheet within document]
    *
    ****************************************************************************/
    FUNCTION add_sheet(sheet_name VARCHAR2,
                       doc_id PLS_INTEGER DEFAULT current_doc_id)
    RETURN PLS_INTEGER;



    /***************************************************************************
    * Description: Wrapper procedure of add_sheet function
    *
    * Input Parameters:
    *   - sheet_name [sheet name that will be displayed on sheet tab]
    *   - doc_id     [unique identificator of document]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE add_sheet(sheet_name VARCHAR2,
                        doc_id PLS_INTEGER DEFAULT current_doc_id);



    /***************************************************************************
    * Description: Adds row to current or specified sheet
    *
    * Input Parameters:
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - row_id    [unique identificator of row within specified sheed and document]
    *
    ****************************************************************************/
    FUNCTION add_row(doc_id PLS_INTEGER DEFAULT current_doc_id,
                     sheet_id PLS_INTEGER DEFAULT current_sheet_id) RETURN PLS_INTEGER;



    /***************************************************************************
    * Description: Wrapper procedure for add_row function
    *
    * Input Parameters:
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE add_row(doc_id PLS_INTEGER DEFAULT current_doc_id,
                      sheet_id PLS_INTEGER DEFAULT current_sheet_id);



    /***************************************************************************
    * Description: Sets height of speficied row
    *
    * Input Parameters:
    *   - height     [height of the row]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_row_height(height NUMBER,
                             doc_id PLS_INTEGER DEFAULT current_doc_id,
                             sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                             row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets value of numeric cell
    *
    * Input Parameters:
    *   - name       [name of the cell where value will be added]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_value (name VARCHAR2,
                              value NUMBER,
                              doc_id PLS_INTEGER DEFAULT current_doc_id,
                              sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                              row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets value of string cell
    *
    * Input Parameters:
    *   - name       [name of the cell where value will be added]
    *   - value      [date value that will be set to the cell]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_value (name VARCHAR2,
                              value VARCHAR2,
                              doc_id PLS_INTEGER DEFAULT current_doc_id,
                              sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                              row_id PLS_INTEGER DEFAULT current_row_id);


    /***************************************************************************
    * Description: Sets value of cell with internal xml string
    *
    * Input Parameters:
    *   - name       [name of the cell where value will be added]
    *   - value      [xml value that will be added to cell]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_internal_code (name VARCHAR2,
                                      value CLOB,
                                      doc_id PLS_INTEGER DEFAULT current_doc_id,
                                      sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                      row_id PLS_INTEGER DEFAULT current_row_id);


    /***************************************************************************
    * Description: Sets value of date cell
    *
    * Input Parameters:
    *   - name       [name of the cell where value will be added]
    *   - value      [date value that will be set to the cell]
    *   - doc_id     [unique identificator of document
    *   - sheet_id   [unique identificator of sheet
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_value (name VARCHAR2,
                              value DATE,
                              doc_id PLS_INTEGER DEFAULT current_doc_id,
                              sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                              row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets value of big string cell (CLOB)
    *
    * Input Parameters:
    *   - name       [name of the cell where value will be added]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_value (name VARCHAR2,
                              value CLOB,
                              doc_id PLS_INTEGER DEFAULT current_doc_id,
                              sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                              row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets format of the cell
    *
    * Input Parameters:
    *   - cell_name  [name of the cell on which format will be applied]
    *   - format     [format mask that will be applied to the cell, same as custom format in Excel]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_format (cell_name VARCHAR2,
                               format VARCHAR2,
                               doc_id PLS_INTEGER DEFAULT current_doc_id,
                               sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                               row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Generates Excel document and stores it in BLOB variable
    *
    * Input Parameters:
    *   - blob_file  [BLOB variable where generated Excel will be stored]
    *   - doc_id     [unique identificator of document]
    *
    * Output Parameters:
    *   - blob_file  [BLOB variable where generated Excel will be stored]
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE save_to_blob(blob_file IN OUT BLOB,
                           doc_id PLS_INTEGER DEFAULT current_doc_id);



    /***************************************************************************
    * Description: Sets font family of the cell
    *
    * Input Parameters:
    *   - cell_name  [name of the cell on which format will be applied]
    *   - font_name  [font family which will be applied to the cell ex. Arial]
    *   - font_size  [size that will be applied to the font for specified cell]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_font(cell_name VARCHAR2,
                            font_name VARCHAR2,
                            font_size PLS_INTEGER DEFAULT 10,
                            doc_id PLS_INTEGER DEFAULT current_doc_id,
                            sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                            row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets default font family for whole document
    *
    * Input Parameters:
    *   - font_name  [font family which will be applied to whole document ex. Arial]
    *   - font_size  [size that will be applied to the font for whole document]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_default_font (font_name VARCHAR2 DEFAULT 'Calibri',
                                font_size PLS_INTEGER DEFAULT 11,
                                doc_id PLS_INTEGER DEFAULT current_doc_id);



    /***************************************************************************
    * Description: Sets cell type to bold
    *
    * Input Parameters:
    *   - name       [name of cell which content will be bolded]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_bold(name VARCHAR2,
                            doc_id PLS_INTEGER DEFAULT current_doc_id,
                            sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                            row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets cell stype to italic
    *
    * Input Parameters:
    *   - name       [name of cell which content will be formated to italic]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_italic(name VARCHAR2,
                              doc_id PLS_INTEGER DEFAULT current_doc_id,
                              sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                              row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets cell stype to underline
    *
    * Input Parameters:
    *   - name       [name of cell which content will be underline]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_underline(name VARCHAR2,
                                 doc_id PLS_INTEGER DEFAULT current_doc_id,
                                 sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                 row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets color of font within cell
    *
    * Input Parameters:
    *   - name       [name of cell which font will be colored]
    *   - color      [font color in RRGGBB hex format RR - red, GG - green, BB blue]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_color(name VARCHAR2,
                             color VARCHAR2,
                             doc_id PLS_INTEGER DEFAULT current_doc_id,
                             sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                             row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets background color of cell
    *
    * Input Parameters:
    *   - name       [name of cell which background will be colored]
    *   - color      [background color in RRGGBB hex format RR - red, GG - green, BB blue]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_bg_color(name VARCHAR2,
                                color VARCHAR2,
                                doc_id PLS_INTEGER DEFAULT current_doc_id,
                                sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets left side horizontal alignement of cell
    *
    * Input Parameters:
    *   - name       [name of cell content will be placed to left side of column]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_align_left(name VARCHAR2,
                                 doc_id PLS_INTEGER DEFAULT current_doc_id,
                                 sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                 row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets horizontal alignement of cell to center
    *
    * Input Parameters:
    *   - name       [name of cell content will be centered]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_align_center(name VARCHAR2,
                                    doc_id PLS_INTEGER DEFAULT current_doc_id,
                                    sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                    row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets horizontal alignement of cell to right
    *
    * Input Parameters:
    *   - name       [name of cell content will be placed to right side of column]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_align_right(name VARCHAR2,
                                   doc_id PLS_INTEGER DEFAULT current_doc_id,
                                   sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                   row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets vertical alignement of cell to top
    *
    * Input Parameters:
    *   - name       [name of cell content will be placed to the top]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_vert_align_top(name VARCHAR2,
                                      doc_id PLS_INTEGER DEFAULT current_doc_id,
                                      sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                      row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets vertical alignement of cell to middle
    *
    * Input Parameters:
    *   - name       [name of cell content will be placed in the middle]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_vert_align_middle(name VARCHAR2,
                                         doc_id PLS_INTEGER DEFAULT current_doc_id,
                                         sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                         row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets vertical alignement of cell to bottom
    *
    * Input Parameters:
    *   - name       [name of cell content will be placed to the bottom]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_vert_align_bottom(name VARCHAR2,
                                         doc_id PLS_INTEGER DEFAULT current_doc_id,
                                         sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                         row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets width of the column
    *
    * Input Parameters:
    *   - name       [name of the column which width will be set to specified value]
    *   - width      [width of column]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_column_width(name VARCHAR2,
                               width NUMBER,
                               doc_id PLS_INTEGER DEFAULT current_doc_id,
                               sheet_id PLS_INTEGER DEFAULT current_sheet_id);



    /***************************************************************************
    * Description: Sets top border of the cell
    *
    * Input Parameters:
    *   - name       [name of cell on which top border will be set]
    *   - style      [style of the border:  thin, thick, double]
    *   - color      [color of border in RRGGBB hex format RR - red, GG - green, BB blue]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_border_top(name VARCHAR2,
                                  style VARCHAR2 DEFAULT 'thin',
                                  color VARCHAR2 DEFAULT '00000000',
                                  doc_id PLS_INTEGER DEFAULT current_doc_id,
                                  sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                  row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets bottom border of the cell
    *
    * Input Parameters:
    *   - name       [name of cell on which bottom border will be set]
    *   - style      [style of the border:  thin, thick, double]
    *   - color      [color of border in RRGGBB hex format RR - red, GG - green, BB blue]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_border_bottom(name VARCHAR2,
                                     style VARCHAR2 DEFAULT 'thin',
                                     color VARCHAR2 DEFAULT '00000000',
                                     doc_id PLS_INTEGER DEFAULT current_doc_id,
                                     sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                     row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets left border of the cell
    *
    * Input Parameters:
    *   - name       [name of cell on which left border will be set]
    *   - style      [style of the border:  thin, thick, double]
    *   - color      [color of border in RRGGBB hex format RR - red, GG - green, BB blue]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_border_left(name VARCHAR2,
                                   style VARCHAR2 DEFAULT 'thin',
                                   color VARCHAR2 DEFAULT '00000000',
                                   doc_id PLS_INTEGER DEFAULT current_doc_id,
                                   sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                   row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets right border of the cell
    *
    * Input Parameters:
    *   - name       [name of cell on which right border will be set]
    *   - style      [style of the border:  thin, thick, double]
    *   - color      [color of border in RRGGBB hex format RR - red, GG - green, BB blue]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_border_right(name VARCHAR2,
                                    style VARCHAR2 DEFAULT 'thin',
                                    color VARCHAR2 DEFAULT '00000000',
                                    doc_id PLS_INTEGER DEFAULT current_doc_id,
                                    sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                    row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets border of the cell
    *
    * Input Parameters:
    *   - name       [name of cell on which border will be set]
    *   - style      [style of the border:  thin, thick, double]
    *   - color      [color of border in RRGGBB hex format RR - red, GG - green, BB blue]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_border(name VARCHAR2,
                              style VARCHAR2 DEFAULT 'thin',
                              color VARCHAR2 DEFAULT '00000000',
                              doc_id PLS_INTEGER DEFAULT current_doc_id,
                              sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                              row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Fetches data with speified query and place results on specified
    * or current sheet
    *
    * Input Parameters:
    *   - query             [SQL query whih result will be added to sheet]
    *   - show_column_names [parameter to hide or show column names from SQL query, boolean values TRUE or FALSE]
    *   - doc_id            [unique identificator of document]
    *   - sheet_id          [unique identificator of sheet]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE query_to_sheet(query CLOB,
                             show_column_names BOOLEAN DEFAULT TRUE,
                             doc_id PLS_INTEGER DEFAULT current_doc_id,
                             sheet_id PLS_INTEGER DEFAULT current_sheet_id);



    /***************************************************************************
    * Description: Generates Excel document and saves it to physical file in Oracle directory
    *
    * Input Parameters:
    *   - directory_name [name of Oracle directory where Excel document will be saved]
    *   - file_name      [file name of generated Excel document]
    *   - doc_id         [unique identificator of document]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE save_to_file(directory_name VARCHAR2,
                           file_name VARCHAR2,
                           doc_id PLS_INTEGER DEFAULT current_doc_id);



    /***************************************************************************
    * Description: Merges cells horizontaly within specified range
    *
    * Input Parameters:
    *   - cell_from  [name of cell from merge will be started, name example: A1]
    *   - cell_to    [name of cell to where merge will finis, name example: C1]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE merge_cells (cell_from VARCHAR2,
                           cell_to VARCHAR2,
                           doc_id PLS_INTEGER DEFAULT current_doc_id,
                           sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                           row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Wraps text within cell
    *
    * Input Parameters:
    *   - name       [name of cell which text will be wrapped]
    *   - doc_id     [unique identificator of document on which is located sheet
    *                 where is lcaterd row with cell which conted will be wrapped]
    *   - sheet_id   [unique identificator of sheet on which is located row
    *                 row with cell which conted will be wrapped]
    *   - row_id     [unique identificator of row on which cell will be merged]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_wrap_text (name VARCHAR2,
                                  doc_id PLS_INTEGER DEFAULT current_doc_id,
                                  sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                  row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Merges cells vertically within specified range
    *
    * Input Parameters:
    *   - cell_from  [name of cell from merge will be started, name example: A1]
    *   - cell_to    [name of cell to where merge will finis, name example: C1]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE merge_rows (cell_from VARCHAR2,
                          cell_to PLS_INTEGER,
                          doc_id PLS_INTEGER DEFAULT current_doc_id,
                          sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                          row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Sets cell formula
    *
    * Input Parameters:
    *   - name       [name of the cell where value will be added]
    *   - formula    [formula that will be used to calculate cell value]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
        PROCEDURE set_cell_formula(name VARCHAR2,
                                  formula VARCHAR2,
                                  doc_id PLS_INTEGER DEFAULT current_doc_id,
                                  sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                  row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Rotates text to a diagonal angle
    *
    * Input Parameters:
    *   - name       [name of cell content will be centered]
    *   - degrees    [degree from 90 to 180 which will be used to rotate text]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_rotate_text(name VARCHAR2,
                                   degrees INTEGER,
                                   doc_id PLS_INTEGER DEFAULT current_doc_id,
                                   sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                   row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Initiates download of generated excel file using DAD
    *
    * Input Parameters:
    *   - file_name      [file name that will be suggested when download dialog appears]
    *   - doc_id         [unique identificator of document]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE download_file(file_name VARCHAR2,
                            doc_id PLS_INTEGER DEFAULT current_doc_id);



    /***************************************************************************
    * Description: Sets margins of sheet
    *
    * Input Parameters:
    *   - left_margin     [margin size on the left side of sheet]
    *   - right_margin    [margin size on the right side of sheet]
    *   - top_margin      [margin size on the top side of sheet]
    *   - bottom_margin   [margin size on the bottom side of sheet]
    *   - header_margin   [margin size on the header side of sheet]
    *   - footer_margin   [margin size on the footer side of sheet]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_sheet_margins(left_margin NUMBER,
                                right_margin NUMBER,
                                top_margin NUMBER,
                                bottom_margin NUMBER,
                                header_margin NUMBER,
                                footer_margin NUMBER,
                                sheet_id PLS_INTEGER DEFAULT current_sheet_id);


    /***************************************************************************
    * Description: Sets sheet orientation to landscape
    *
    * Input Parameters:
    *   - sheet_id   [unique identificator of sheet]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_sheet_landscape(sheet_id PLS_INTEGER DEFAULT current_sheet_id);


    /***************************************************************************
    * Description: Sets sheet paper size
    *
    * Input Parameters:
    *   - paper_size [paper size of sheet]
    *                Possible values:
    *                   1 - Letter (8-1/2 in. x 11 in.)
    *                   2 - Letter Small (8-1/2 in. x 11 in.)
    *                   3 - Tabloid (11 in. x 17 in.)
    *                   4 - Ledger (17 in. x 11 in.)
    *                   5 - Legal (8-1/2 in. x 14 in.)
    *                   6 - Statement (5-1/2 in. x 8-1/2 in.)
    *                   7 - Executive (7-1/2 in. x 10-1/2 in.)
    *                   8 - A3 (297 mm x 420 mm)
    *                   9 - A4 (210 mm x 297 mm)
    *                   10 - A4 Small (210 mm x 297 mm)
    *                   11 - A5 (148 mm x 210 mm)
    *                   12 - B4 (250 mm x 354 mm)
    *                   13 - A5 (148 mm x 210 mm)
    *                   14 - Folio (8-1/2 in. x 13 in.)
    *                   15 - Quarto (215 mm x 275 mm)
    *                   16 - 10 in. x 14 in.
    *                   17 - 11 in. x 17 in.
    *                   18 - Note (8-1/2 in. x 11 in.)
    *                   19 - Envelope #9 (3-7/8 in. x 8-7/8 in.)
    *                   20 - Envelope #10 (4-1/8 in. x 9-1/2 in.)
    *                   21 - Envelope #11 (4-1/2 in. x 10-3/8 in.)
    *                   22 - Envelope #12 (4-1/2 in. x 11 in.)
    *                   23 - Envelope #14 (5 in. x 11-1/2 in.)
    *                   24 - C size sheet
    *                   25 - D size sheet
    *                   26 - E size sheet
    *                   27 - Envelope DL (110 mm x 220 mm)
    *                   28 - Envelope C5 (162 mm x 229 mm)
    *                   29 - Envelope C3 (324 mm x 458 mm)
    *                   30 - Envelope C4 (229 mm x 324 mm)
    *                   31 - Envelope C6 (114 mm x 162 mm)
    *                   32 - Envelope C65 (114 mm x 229 mm)
    *                   33 - Envelope B4 (250 mm x 353 mm)
    *                   34 - Envelope B5 (176 mm x 250 mm)
    *                   35 - Envelope B6 (176 mm x 125 mm)
    *                   36 - Envelope (110 mm x 230 mm)
    *                   37 - Envelope Monarch (3-7/8 in. x 7-1/2 in.)
    *                   38 - Envelope (3-5/8 in. x 6-1/2 in.)
    *                   39 - U.S. Standard Fanfold (14-7/8 in. x 11 in.)
    *                   40 - German Legal Fanfold (8-1/2 in. x 13 in.)
    *                   41 - German Legal Fanfold (8-1/2 in. x 13 in.)
    *   - sheet_id   [unique identificator of sheet]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_sheet_paper_size(paper_size INTEGER,
                                   sheet_id PLS_INTEGER DEFAULT current_sheet_id);


    /***************************************************************************
    * Description: Sets sheet header text
    *
    * Input Parameters:
    *   - header_text [text that will be displayed on sheets header, limited to 1000 characters]
    *   - sheet_id    [unique identificator of sheet]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_sheet_header_text(header_text VARCHAR2,
                                    sheet_id PLS_INTEGER DEFAULT current_sheet_id);


    /***************************************************************************
    * Description: Sets sheet footer text
    *
    * Input Parameters:
    *   - header_text [text that will be displayed on sheets footer, limited to 1000 characters]
    *   - sheet_id    [unique identificator of sheet]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_sheet_footer_text(footer_text VARCHAR2,
                                    sheet_id PLS_INTEGER DEFAULT current_sheet_id);


    /***************************************************************************
    * Description: Sets hyperlink for cell
    *
    * Input Parameters:
    *   - name       [cell name]
    *   - hyperlink  [hyperlink that will be set on cell]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_hyperlink(name VARCHAR2,
                                 hyperlink VARCHAR2,
                                 doc_id PLS_INTEGER DEFAULT current_doc_id,
                                 sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                 row_id PLS_INTEGER DEFAULT current_row_id);


    /***************************************************************************
    * Description: Sets hyperlink within document, to same or another sheet and
    *              to cell within sheet
    *
    * Input Parameters:
    *   - name       [cell name]
    *   - hyperlink  [hyperlink that will be set on cell, example Sheet1!A1 will
    *                 link to sheet with name Sheet1 and to cell A1 within that
    *                 sheet - if sheet name containst spaces it must be enclosed
    *                 with sigle quotes example: '''My Sheet1''!A1' ]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_internal_hyperlink(name VARCHAR2,
                                          hyperlink VARCHAR2,
                                          doc_id PLS_INTEGER DEFAULT current_doc_id,
                                          sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                          row_id PLS_INTEGER DEFAULT current_row_id);


    /***************************************************************************
    * Description: Sets left indent within the cell
    *
    * Input Parameters:
    *   - name       [name of cell content will be indented from the left side]
    *   - indent     [number of indent from left site of cell]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_indent_left(name VARCHAR2,
                                   indent INTEGER,
                                   doc_id PLS_INTEGER DEFAULT current_doc_id,
                                   sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                   row_id PLS_INTEGER DEFAULT current_row_id);


    /***************************************************************************
    * Description: Sets right indent within the cell
    *
    * Input Parameters:
    *   - name       [name of cell content will be indented from the right side]
    *   - indent     [number of indent from left site of cell]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_indent_right(name VARCHAR2,
                                   indent INTEGER,
                                   doc_id PLS_INTEGER DEFAULT current_doc_id,
                                   sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                                   row_id PLS_INTEGER DEFAULT current_row_id);


    /***************************************************************************
    * Description: Sets comment text and author name for cell
    *
    * Input Parameters:
    *   - name                [cell name]
    *   - author              [name of the autor of the comment]
    *   - comment_text        [comment text for the cell]
    *   - comment_box_width   [width of comment box]
    *   - comment_box_height  [height of comment box]
    *   - doc_id              [unique identificator of document]
    *   - sheet_id            [unique identificator of sheet]
    *   - row_id              [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_comment(name VARCHAR2,
                               autohr VARCHAR2,
                               comment_text VARCHAR2,
                               comment_box_width NUMBER DEFAULT 100,
                               comment_box_height NUMBER DEFAULT 60,
                               doc_id PLS_INTEGER DEFAULT current_doc_id,
                               sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                               row_id PLS_INTEGER DEFAULT current_row_id);


    /***************************************************************************
    * Description: Hides column
    *
    * Input Parameters:
    *   - name       [name of the column which will be hidden]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE hide_column(name VARCHAR2,
                          doc_id PLS_INTEGER DEFAULT current_doc_id,
                          sheet_id PLS_INTEGER DEFAULT current_sheet_id);


    /***************************************************************************
    * Description: Hides row
    *
    * Input Parameters:
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *   - row_id     [unique identificator of row]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE hide_row(doc_id PLS_INTEGER DEFAULT current_doc_id,
                       sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                       row_id PLS_INTEGER DEFAULT current_row_id);


    /***************************************************************************
    * Description: Sets column auto filter between defined columns range
    *
    * Input Parameters:
    *   - cell_from  [cell name with row number from which auto filter will start, example: A1]
    *   - cell_to    [cell name with row number where auto filter will end, example: A5]
    *   - doc_id     [unique identificator of document]
    *   - sheet_id   [unique identificator of sheet]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cells_filter(cell_from VARCHAR2,
                              cell_to VARCHAR2,
                              doc_id PLS_INTEGER DEFAULT current_doc_id,
                              sheet_id PLS_INTEGER DEFAULT current_sheet_id);


    /***************************************************************************
    * Description: Opens Excel file for reading
    *
    * Input Parameters:
    *   - directory_name [name of Oracle directory from where Excel document will be opened]
    *   - file_name      [file name of Excel document]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
     PROCEDURE open_document(directory_name VARCHAR2,
                             file_name VARCHAR2);


    /***************************************************************************
    * Description: Release memory for currently opened document
    *
    * Input Parameters:
    *   - no imput parameters
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
     PROCEDURE close_document;


    /***************************************************************************
    * Description: Loads sheet from opened document
    *
    * Input Parameters:
    *   - sheet_name - [name of sheet case isensitive]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE open_sheet(sheet_name VARCHAR2);


    /***************************************************************************
    * Description: Loads sheet from opened document
    *
    * Input Parameters:
    *   - sheet_id - [numberic index od sheet (ex. 1 = first sheet, 2 = second sheet ...)]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE open_sheet(sheet_id PLS_INTEGER);


    /***************************************************************************
    * Description: Returns cell value
    *
    * Input Parameters:
    *   - cell_name - [cell name which conted will be returned (ex. A1)]
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - cell value type
    *       - varchar2_value  - cell value converted to varchar2 type
    *       - varchar2_number - cell value converted to number type
    *       - varchar2_date   - cell value converted to date type
    *
    ****************************************************************************/

    FUNCTION get_cell_value(cell_name VARCHAR2) RETURN cell_value_type;


    /***************************************************************************
    * Description: Returns last row number from loaded sheet
    *
    * Input Parameters:
    *   - no input parameters
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - last row number
    *
    ****************************************************************************/
    FUNCTION get_last_row RETURN INTEGER;


    /***************************************************************************
    * Description: Set 1904 date system, the first day that is supported is
    *              January 1, 1904
    *
    * Input Parameters:
    *   - no input parameters
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_1904_date_system;


    /***************************************************************************
    * Description: Set 1900 date system, the first day that is supported is
    *              January 1, 1900
    *
    * Input Parameters:
    *   - no input parameters
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_1900_date_system;

    /***************************************************************************
    * Description: Set document author property to custom value
    *
    * Input Parameters:
    *   - author - author name
    *   - doc_id - unique identificator of document
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/

    PROCEDURE set_document_author(author VARCHAR2, doc_id PLS_INTEGER DEFAULT current_doc_id);


    /***************************************************************************
    * Description: Add style to current docuemnt
    *
    * Input Parameters:
    *   - style_name            - style name used as referece to apply to cell
    *   - font_name             - font name examples: 'Tahoma', 'Arial', 'Times New Roman'
    *   - font_size             - font size, default 11
    *   - formula               - cell formula
    *   - bold                  - bold cell content
    *   - italic                - italic cell content
    *   - underline             - underline cell content
    *   - color                 - text color in hex, example: red = FF0000
    *   - bg_color              - cell background color in hex, example: grey = CCCCCC
    *   - horizontal_align      - horizontal text alignment, values: 'left', 'center', 'right'
    *   - vertical_align        - vertical text alignment, value: 'top', 'middle', 'bottom'
    *   - border_top            - show cell top border, values: TRUE, FALSE
    *   - border_top_style      - top border style, values: 'thin', 'thick', 'double'
    *   - border_top_color      - top border color in hex, example green = 00FF00
    *   - border_bottom         - show cell bottom border, values: TRUE, FALSE
    *   - border_bottom_style   - bottom border style, values: 'thin', 'thick', 'double'
    *   - border_bottom_color   - bottom border color in hex, example green = 00FF00
    *   - border_left           - show cell left border, values: TRUE, FALSE
    *   - border_left_style     - left border style, values: 'thin', 'thick', 'double'
    *   - border_left_color     - left border color in hex, example green = 00FF00
    *   - border_right          - show cell right border, values: TRUE, FALSE
    *   - border_right_style    - right border style, values: 'thin', 'thick', 'double'
    *   - border_right_color    - right border color in hex, example green = 00FF00
    *   - border                - show all cell borders, values: TRUE, FALSE
    *   - border_style          - all cell borders style, values: 'thin', 'thick', 'double'
    *   - border_color          - all cell borders color in hex, example green = 00FF00
    *   - wrap_text             - wrap text within cell, values: TRUE, FALSE
    *   - format                - format mas for cell
    *   - rotate_text_degree    - degree number for text rotation, values from 0 to 360
    *   - indent_left           - number of indents from left side, values: number greather than zero
    *   - indent_right          - Number of indents from right side, values: number greather than zero
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE add_style (style_name VARCHAR2,
                         font_name VARCHAR2 DEFAULT NULL,
                         font_size PLS_INTEGER DEFAULT NULL,
                         formula VARCHAR2 DEFAULT NULL,
                         bold BOOLEAN DEFAULT FALSE,
                         italic BOOLEAN DEFAULT FALSE,
                         underline BOOLEAN DEFAULT FALSE,
                         color VARCHAR2 DEFAULT NULL,
                         bg_color VARCHAR2 DEFAULT NULL,
                         horizontal_align VARCHAR2 DEFAULT NULL,
                         vertical_align VARCHAR2 DEFAULT NULL,
                         border_top BOOLEAN DEFAULT FALSE,
                         border_top_style VARCHAR2 DEFAULT NULL,
                         border_top_color VARCHAR2 DEFAULT NULL,
                         border_bottom BOOLEAN DEFAULT FALSE,
                         border_bottom_style VARCHAR2 DEFAULT NULL,
                         border_bottom_color VARCHAR2 DEFAULT NULL,
                         border_left BOOLEAN DEFAULT FALSE,
                         border_left_style VARCHAR2 DEFAULT NULL,
                         border_left_color VARCHAR2 DEFAULT NULL,
                         border_right BOOLEAN DEFAULT FALSE,
                         border_right_style VARCHAR2 DEFAULT NULL,
                         border_right_color VARCHAR2 DEFAULT NULL,
                         border BOOLEAN DEFAULT NULL,
                         border_style VARCHAR2 DEFAULT NULL,
                         border_color VARCHAR2 DEFAULT NULL,
                         wrap_text BOOLEAN DEFAULT FALSE,
                         format VARCHAR2 DEFAULT NULL,
                         rotate_text_degree INTEGER DEFAULT NULL,
                         indent_left INTEGER DEFAULT NULL,
                         indent_right INTEGER DEFAULT NULL,
                         column_width INTEGER DEFAULT NULL,
                         doc_id PLS_INTEGER DEFAULT current_doc_id);

    /***************************************************************************
    * Description: Applies predefined style to cell
    *
    * Input Parameters:
    *   - cell_name  - name of cell, example 'A'
    *   - style_name - name of style defined with procedure add_style
    *   - doc_id     - unique identificator of document
    *   - sheet_id   - unique identificator of sheet
    *   - row_id     - unique identificator of row
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/

    PROCEDURE set_cell_style(cell_name VARCHAR2,
                             style_name VARCHAR2,
                             doc_id PLS_INTEGER DEFAULT current_doc_id,
                             sheet_id PLS_INTEGER DEFAULT current_sheet_id,
                             row_id PLS_INTEGER DEFAULT current_row_id);



    /***************************************************************************
    * Description: Freeze panes horizontally
    *
    * Input Parameters:
    *   - freeze_columns_number  - top left column name with row number,
    *                              example: to freeze first row set 1 as top
    *                              left row
    *   - doc_id                 - unique identificator of document
    *   - sheet_id               - unique identificator of sheet
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE freeze_panes_horizontal(freeze_columns_number VARCHAR2,
                                      doc_id PLS_INTEGER DEFAULT current_doc_id,
                                      sheet_id PLS_INTEGER DEFAULT current_sheet_id);



    /***************************************************************************
    * Description: Freeze panes vertically
    *
    * Input Parameters:
    *   - freeze_rows_number  - top left column name with row number, example:
    *                           to freeze first column set 1 as top left column
    *   - doc_id              - unique identificator of document
    *   - sheet_id            - unique identificator of sheet
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE freeze_panes_vertical(freeze_rows_number VARCHAR2,
                                    doc_id PLS_INTEGER DEFAULT current_doc_id,
                                    sheet_id PLS_INTEGER DEFAULT current_sheet_id);

    /***************************************************************************
    * Description: Save content of BLOB variable to file
    *
    * Input Parameters:
    *   - blob_content      - BLOB variable which content will be saved to file
    *   - directory_name    - Oracle directory where BLOB content will be saved
    *   - file_name         - Name of file which will be created
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE blob_to_file (blob_content BLOB,
                            directory_name VARCHAR2,
                            file_name VARCHAR2);

    /***************************************************************************
    * Description: Group columns
    *
    * Input Parameters:
    *   - column_name_from  - Name of column to start group from, example 'A'
    *   - column_name_to    - Name of column to end group to, example 'A'
    *   - group_level       - Group level, example 1
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE group_columns(column_name_from VARCHAR2,
                            column_name_to VARCHAR2,
                            group_level NUMBER,
                            doc_id PLS_INTEGER DEFAULT current_doc_id,
                            sheet_id PLS_INTEGER DEFAULT current_sheet_id);

    /***************************************************************************
    * Description: Set cell validation by provided list of values
    *
    * Input Parameters:
    *   - column_name           - Name of column to set list validation
    *   - formula               - Formula used to locate list of strings gto compare with
    *   - error_message_title   - Title text for error message if entered value is not valid
    *   - error_message_text    - Error mesage text
    *   - prompt_message_title  - Title for message which is displayed when cell is selected
    *   - prompt_message_text   - Message text displayed when cell is selected
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_validation_list(column_name VARCHAR2,
                                       formula VARCHAR2,
                                       error_message_title VARCHAR2,
                                       error_message_text VARCHAR2,
                                       prompt_message_title VARCHAR2,
                                       prompt_message_text VARCHAR2,
                                       doc_id PLS_INTEGER DEFAULT current_doc_id,
                                       sheet_id PLS_INTEGER DEFAULT current_sheet_id);

    /***************************************************************************
    * Description: Set cell validation by whole number
    *
    * Input Parameters:
    *   - column_name           - Name of column to set whole number validation
    *   - comparison_operator   - Comparison operator (between, notbetween, equal, notequal,
    *                             greaterthan, lessthan, greaterthanorequal, lessthanorequal)
    *   - formula_min           - Number of formula for minimum value
    *   - formula_max           - Number of formula for maximum value
    *   - error_message_title   - Title text for error message if entered value is not valid
    *   - error_message_text    - Error mesage text
    *   - prompt_message_title  - Title for message which is displayed when cell is selected
    *   - prompt_message_text   - Message text displayed when cell is selected
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_validation_integer(column_name VARCHAR2,
                                          comparison_operator VARCHAR2,
                                          formula_min VARCHAR2,
                                          formula_max VARCHAR2,
                                          error_message_title VARCHAR2,
                                          error_message_text VARCHAR2,
                                          prompt_message_title VARCHAR2,
                                          prompt_message_text VARCHAR2,
                                          doc_id PLS_INTEGER DEFAULT current_doc_id,
                                          sheet_id PLS_INTEGER DEFAULT current_sheet_id);

    /***************************************************************************
    * Description: Set cell validation by decimal number
    *
    * Input Parameters:
    *   - column_name           - Name of column to set decimal number validation
    *   - comparison_operator   - Comparison operator (between, notbetween, equal, notequal,
    *                             greaterthan, lessthan, greaterthanorequal, lessthanorequal)
    *   - formula_min           - Number of formula for minimum value
    *   - formula_max           - Number of formula for maximum value
    *   - error_message_title   - Title text for error message if entered value is not valid
    *   - error_message_text    - Error mesage text
    *   - prompt_message_title  - Title for message which is displayed when cell is selected
    *   - prompt_message_text   - Message text displayed when cell is selected
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_validation_decimal(column_name VARCHAR2,
                                          comparison_operator VARCHAR2,
                                          formula_min VARCHAR2,
                                          formula_max VARCHAR2,
                                          error_message_title VARCHAR2,
                                          error_message_text VARCHAR2,
                                          prompt_message_title VARCHAR2,
                                          prompt_message_text VARCHAR2,
                                          doc_id PLS_INTEGER DEFAULT current_doc_id,
                                          sheet_id PLS_INTEGER DEFAULT current_sheet_id);

    /***************************************************************************
    * Description: Set cell validation by date
    *
    * Input Parameters:
    *   - column_name           - Name of column to set date validation
    *   - comparison_operator   - Comparison operator (between, notbetween, equal, notequal,
    *                             greaterthan, lessthan, greaterthanorequal, lessthanorequal)
    *   - date_min              - Minumum date
    *   - date_max              - Maximum date
    *   - error_message_title   - Title text for error message if entered value is not valid
    *   - error_message_text    - Error mesage text
    *   - prompt_message_title  - Title for message which is displayed when cell is selected
    *   - prompt_message_text   - Message text displayed when cell is selected
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_validation_date(column_name VARCHAR2,
                                       comparison_operator VARCHAR2,
                                       date_min DATE,
                                       date_max DATE,
                                       error_message_title VARCHAR2,
                                       error_message_text VARCHAR2,
                                       prompt_message_title VARCHAR2,
                                       prompt_message_text VARCHAR2,
                                       doc_id PLS_INTEGER DEFAULT current_doc_id,
                                       sheet_id PLS_INTEGER DEFAULT current_sheet_id);

    /***************************************************************************
    * Description: Set cell validation by date
    *
    * Input Parameters:
    *   - column_name           - Name of column to set time validation
    *   - comparison_operator   - Comparison operator (between, notbetween, equal, notequal,
    *                             greaterthan, lessthan, greaterthanorequal, lessthanorequal)
    *   - time_min              - Minumum time in format hh24:mi:ss (example: 13:45:20)
    *   - time_max              - Maximum date in format hh24:mi:ss (example: 10:00:00)
    *   - error_message_title   - Title text for error message if entered value is not valid
    *   - error_message_text    - Error mesage text
    *   - prompt_message_title  - Title for message which is displayed when cell is selected
    *   - prompt_message_text   - Message text displayed when cell is selected
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_validation_time(column_name VARCHAR2,
                                       comparison_operator VARCHAR2,
                                       time_min VARCHAR2,
                                       time_max VARCHAR2,
                                       error_message_title VARCHAR2,
                                       error_message_text VARCHAR2,
                                       prompt_message_title VARCHAR2,
                                       prompt_message_text VARCHAR2,
                                       doc_id PLS_INTEGER DEFAULT current_doc_id,
                                       sheet_id PLS_INTEGER DEFAULT current_sheet_id);

    /***************************************************************************
    * Description: Set cell validation by text length
    *
    * Input Parameters:
    *   - column_name           - Name of column to set text length validation
    *   - comparison_operator   - Comparison operator (between, notbetween, equal, notequal,
    *                             greaterthan, lessthan, greaterthanorequal, lessthanorequal)
    *   - formula_min           - Number of formula for minimum text length
    *   - formula_max           - Number of formula for maximum text length
    *   - error_message_title   - Title text for error message if entered value is not valid
    *   - error_message_text    - Error mesage text
    *   - prompt_message_title  - Title for message which is displayed when cell is selected
    *   - prompt_message_text   - Message text displayed when cell is selected
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_cell_validation_text_len(column_name VARCHAR2,
                                           comparison_operator VARCHAR2,
                                           length_min VARCHAR2,
                                           length_max VARCHAR2,
                                           error_message_title VARCHAR2,
                                           error_message_text VARCHAR2,
                                           prompt_message_title VARCHAR2,
                                           prompt_message_text VARCHAR2,
                                           doc_id PLS_INTEGER DEFAULT current_doc_id,
                                           sheet_id PLS_INTEGER DEFAULT current_sheet_id);

    /***************************************************************************
    * Description: Set active cell within sheet - selected cell when document is opened
    *
    * Input Parameters:
    *   - cell_name             - Active cell name (example: C5)
    *   - doc_id                - Unique identificator of document
    *   - sheet_id              - Unique identificator of sheet
    *
    * Output Parameters:
    *   - no output parameters
    *
    * Returns:
    *   - does not return anything
    *
    ****************************************************************************/
    PROCEDURE set_active_cell(cell_name VARCHAR2,
                              doc_id PLS_INTEGER DEFAULT current_doc_id,
                              sheet_id PLS_INTEGER DEFAULT current_sheet_id);
END ORA_EXCEL;
/

