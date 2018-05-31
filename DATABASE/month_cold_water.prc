create or replace procedure month_cold_water
AS
-- Определить месяц для заголовка (d_month)
d_month   varchar2(24) := to_char(sysdate, 'month', 'NLS_DATE_LANGUAGE=RUSSIAN');
-- Определить начальную дату отчета (24 число предыдущего месяца)
d_start   varchar2(24) := '24.'||to_char(add_months(sysdate,-1),'mm.yyyy')||' 00:00:00';
-- Определить конечную дату отчета (25 число текущего месяца)
--d_end     varchar2(24) := '25.'||to_char(add_months(sysdate,-1),'mm.yyyy')||' 23:59:59';
d_end     varchar2(24) := '25.'||to_char(sysdate,'mm.yyyy')||' 23:59:59';

BEGIN

    ORA_EXCEL.new_document;
    ORA_EXCEL.set_1900_date_system;
--    ORA_EXCEL.set_1904_date_system; -- на сутки вперед!

    ORA_EXCEL.add_sheet('куб. м/мес.');

    ora_excel.set_column_width('A',20); -- cshort
    ora_excel.set_column_width('B',60); -- caddress
    ora_excel.set_column_width('C',20); -- куб. м

/*----------------- заголовок -----------------*/
    ORA_EXCEL.add_row;
    ORA_EXCEL.add_row;

    ORA_EXCEL.set_cell_value('B', 'Потребление холодной воды в куб. м за период: '||d_month||' - '||d_start||' - '||d_end);
--    ORA_EXCEL.set_cell_value('B', 'Потребление холодной воды в куб. м за период: ');
    ORA_EXCEL.set_cell_align_center('B');
    ORA_EXCEL.set_cell_bold('B');
    --ORA_EXCEL.set_cell_color('B', 'FF0000');
/*----------------- заголовок -----------------*/

    ORA_EXCEL.add_row;

    ORA_EXCEL.set_cell_value('A', 'Наименование');
    ORA_EXCEL.set_cell_align_center('A');
    ORA_EXCEL.set_cell_bold('A');
    ORA_EXCEL.set_cell_bg_color('A', '999999');

    ORA_EXCEL.set_cell_value('B', 'Адрес');
    ORA_EXCEL.set_cell_align_center('B');
    ORA_EXCEL.set_cell_bold('B');
    ORA_EXCEL.set_cell_bg_color('B', '999999');

    ORA_EXCEL.set_cell_value('C', 'Куб. м (V4)');
    ORA_EXCEL.set_cell_align_center('C');
    ORA_EXCEL.set_cell_bold('C');
    ORA_EXCEL.set_cell_bg_color('C', '999999');

    for rec IN (
    select bb.cshort as "Узел",
           bb.caddress as "Адрес",
           trunc(sum(dc.v4),2) as "Куб. м"
    from datacurr dc, bbuildings bb, bdevices bd where
         dc.id_bd=bd.id_bd AND bd.id_bu=bb.id_bu AND dc.id_ptype=4
         AND dc.id_bd IN (20,58,49,57,53,103,284,28,5,162,161,26,17,23,61,51,98,294,295,141,155,10,3,50,42,60,30,11,41,48)
--         AND dcounter >= to_date('24.10.2016 00:00:00','dd.mm.yyyy hh24:mi:ss')
         AND dcounter >= to_date(d_start,'dd.mm.yyyy hh24:mi:ss')
--         AND dcounter <= to_date('23.11.2016 23:59:59','dd.mm.yyyy hh24:mi:ss')
         AND dcounter <= to_date(d_end,'dd.mm.yyyy hh24:mi:ss') 
         group by bd.id_bd, bb.cshort,bb.caddress
         ORDER BY bb.cshort)
    LOOP

    ORA_EXCEL.add_row;

    ORA_EXCEL.set_cell_value('A', rec."Узел");
    ORA_EXCEL.set_cell_align_left('A');

    ORA_EXCEL.set_cell_value('B', rec."Адрес");
    ORA_EXCEL.set_cell_bold('B');

    ORA_EXCEL.set_cell_value('C', rec."Куб. м");
    ORA_EXCEL.set_cell_bold('C');

  END LOOP;



  ORA_EXCEL.save_to_file('EXPORT_DIR', 'Cold_water_' || to_char(sysdate,'YYMMDD_hh24MISS') || '.xlsx');

END;
/

