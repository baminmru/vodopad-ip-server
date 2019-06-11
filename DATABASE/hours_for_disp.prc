create or replace procedure hours_for_disp
AUTHID CURRENT_USER
IS

BEGIN
    ORA_EXCEL.new_document;
    ORA_EXCEL.set_1900_date_system;
--    ORA_EXCEL.set_1904_date_system; -- на сутки вперед!

    ORA_EXCEL.add_sheet('Часовые за сутки T1-T2');

    ora_excel.set_column_width('A',20);
    ora_excel.set_column_width('B',20);
    ora_excel.set_column_width('C',40);
    ora_excel.set_column_width('D',10); -- T1
    ora_excel.set_column_width('E',10); -- T2
    ora_excel.set_column_width('F',40);

    ORA_EXCEL.add_row;

    ORA_EXCEL.set_cell_value('A', 'Дата');
    ORA_EXCEL.set_cell_align_center('A');
    ORA_EXCEL.set_cell_bold('A');

    ORA_EXCEL.set_cell_value('B', 'Узел');
    ORA_EXCEL.set_cell_align_center('B');
    ORA_EXCEL.set_cell_bold('B');

    ORA_EXCEL.set_cell_value('C', 'Адрес');
    ORA_EXCEL.set_cell_align_center('C');
    ORA_EXCEL.set_cell_bold('C');

    ORA_EXCEL.set_cell_value('D', 'Т1');
    ORA_EXCEL.set_cell_align_center('D');
    ORA_EXCEL.set_cell_bold('D');

    ORA_EXCEL.set_cell_value('E', 'Т2');
    ORA_EXCEL.set_cell_align_center('E');
    ORA_EXCEL.set_cell_bold('E');

    ORA_EXCEL.set_cell_value('F', 'Ответственный');
    ORA_EXCEL.set_cell_align_center('F');
    ORA_EXCEL.set_cell_bold('F');

    for rec IN (
      select "Дата",
             "Узел",
             "Адрес",
             T1,
             T2,
             "Ответственный"
      from v_hours_for
      )
    LOOP

    ORA_EXCEL.add_row;

    ORA_EXCEL.set_cell_value('A', rec."Дата");
    ORA_EXCEL.set_cell_format('A', 'DD.MM.YYYY HH:M');
    ORA_EXCEL.set_cell_align_left('A');

    ORA_EXCEL.set_cell_value('B', rec."Узел");
    ORA_EXCEL.set_cell_bold('B');

    ORA_EXCEL.set_cell_value('C', rec."Адрес");
    ORA_EXCEL.set_cell_bold('C');

    ORA_EXCEL.set_cell_value('D', rec.T1);
    ORA_EXCEL.set_cell_align_left('D');

    ORA_EXCEL.set_cell_value('E', rec.T2);
    ORA_EXCEL.set_cell_align_left('E');

    ORA_EXCEL.set_cell_value('F', rec."Ответственный");

  END LOOP;
/* ========= Часовые за сутки T3 гвс ======================= */

    ORA_EXCEL.add_sheet('Часовые за сутки ГВС T3,T4,T5');

    ora_excel.set_column_width('A',20);
    ora_excel.set_column_width('B',20);
    ora_excel.set_column_width('C',40);
    ora_excel.set_column_width('D',10); -- T3
    ora_excel.set_column_width('E',40);

    ORA_EXCEL.add_row;

    ORA_EXCEL.set_cell_value('A', 'Дата');
    ORA_EXCEL.set_cell_align_center('A');
    ORA_EXCEL.set_cell_bold('A');

    ORA_EXCEL.set_cell_value('B', 'Узел');
    ORA_EXCEL.set_cell_align_center('B');
    ORA_EXCEL.set_cell_bold('B');

    ORA_EXCEL.set_cell_value('C', 'Адрес');
    ORA_EXCEL.set_cell_align_center('C');
    ORA_EXCEL.set_cell_bold('C');

    ORA_EXCEL.set_cell_value('D', 'Т3');
    ORA_EXCEL.set_cell_align_center('D');
    ORA_EXCEL.set_cell_bold('D');

    ORA_EXCEL.set_cell_value('E', 'Ответственный');
    ORA_EXCEL.set_cell_align_center('E');
    ORA_EXCEL.set_cell_bold('E');

    for rec IN (
      select "Дата",
             "Узел",
             "Адрес",
             T3,
             "Ответственный"
      from v_hours_for where T3 <> 0 AND T3 < 120
      )
    LOOP

    ORA_EXCEL.add_row;

    ORA_EXCEL.set_cell_value('A', rec."Дата");
    ORA_EXCEL.set_cell_format('A', 'DD.MM.YYYY HH:M');
    ORA_EXCEL.set_cell_align_left('A');

    ORA_EXCEL.set_cell_value('B', rec."Узел");
    ORA_EXCEL.set_cell_bold('B');

    ORA_EXCEL.set_cell_value('C', rec."Адрес");
    ORA_EXCEL.set_cell_bold('C');

    ORA_EXCEL.set_cell_value('D', rec.T3);
    ORA_EXCEL.set_cell_align_left('D');

    ORA_EXCEL.set_cell_value('E', rec."Ответственный");

  END LOOP;

/* ========= Часовые за сутки T4 гвс ======================= */
/*
    ORA_EXCEL.add_sheet('Часовые за сутки T4 гвс + T5 гвс');
*/
    ORA_EXCEL.add_row;
    ORA_EXCEL.add_row;

    ora_excel.set_column_width('A',20);
    ora_excel.set_column_width('B',20);
    ora_excel.set_column_width('C',40);
    ora_excel.set_column_width('D',10); -- T4
    ora_excel.set_column_width('E',40);

    ORA_EXCEL.add_row;

    ORA_EXCEL.set_cell_value('A', 'Дата');
    ORA_EXCEL.set_cell_align_center('A');
    ORA_EXCEL.set_cell_bold('A');

    ORA_EXCEL.set_cell_value('B', 'Узел');
    ORA_EXCEL.set_cell_align_center('B');
    ORA_EXCEL.set_cell_bold('B');

    ORA_EXCEL.set_cell_value('C', 'Адрес');
    ORA_EXCEL.set_cell_align_center('C');
    ORA_EXCEL.set_cell_bold('C');

    ORA_EXCEL.set_cell_value('D', 'Т4');
    ORA_EXCEL.set_cell_align_center('D');
    ORA_EXCEL.set_cell_bold('D');

    ORA_EXCEL.set_cell_value('E', 'Ответственный');
    ORA_EXCEL.set_cell_align_center('E');
    ORA_EXCEL.set_cell_bold('E');

    for rec IN (
      select "Дата",
             "Узел",
             "Адрес",
             T4,
             "Ответственный"
      from v_hours_for where T4 <> 0 AND T4 < 120
      )
    LOOP

    ORA_EXCEL.add_row;

    ORA_EXCEL.set_cell_value('A', rec."Дата");
    ORA_EXCEL.set_cell_format('A', 'DD.MM.YYYY HH:M');
    ORA_EXCEL.set_cell_align_left('A');

    ORA_EXCEL.set_cell_value('B', rec."Узел");
    ORA_EXCEL.set_cell_bold('B');

    ORA_EXCEL.set_cell_value('C', rec."Адрес");
    ORA_EXCEL.set_cell_bold('C');

    ORA_EXCEL.set_cell_value('D', rec.T4);
    ORA_EXCEL.set_cell_align_left('D');

    ORA_EXCEL.set_cell_value('E', rec."Ответственный");

  END LOOP;

  
    /* ========= Часовые за сутки T5 гвс ======================= */
  /* Procedure add_sheet -20100 ORA-20100: Function add_sheet -20100 ORA-20100: 
     Free version is limited to 3 sheets /
  ---------------------------------------------------------------   */
--    ORA_EXCEL.add_sheet('Часовые за сутки T5 гвс');

    ORA_EXCEL.add_row;
    ORA_EXCEL.add_row;

    ora_excel.set_column_width('A',20);
    ora_excel.set_column_width('B',20);
    ora_excel.set_column_width('C',40);
    ora_excel.set_column_width('D',10); -- T5
    ora_excel.set_column_width('E',40);

    ORA_EXCEL.add_row;

    ORA_EXCEL.set_cell_value('A', 'Дата');
    ORA_EXCEL.set_cell_align_center('A');
    ORA_EXCEL.set_cell_bold('A');

    ORA_EXCEL.set_cell_value('B', 'Узел');
    ORA_EXCEL.set_cell_align_center('B');
    ORA_EXCEL.set_cell_bold('B');

    ORA_EXCEL.set_cell_value('C', 'Адрес');
    ORA_EXCEL.set_cell_align_center('C');
    ORA_EXCEL.set_cell_bold('C');

    ORA_EXCEL.set_cell_value('D', 'T5');
    ORA_EXCEL.set_cell_align_center('D');
    ORA_EXCEL.set_cell_bold('D');

    ORA_EXCEL.set_cell_value('E', 'Ответственный');
    ORA_EXCEL.set_cell_align_center('E');
    ORA_EXCEL.set_cell_bold('E');

    for rec IN (
      select "Дата",
             "Узел",
             "Адрес",
             T5,
             "Ответственный"
      from v_hours_for where T5 <> 0 AND T5 < 120
      )
    LOOP

    ORA_EXCEL.add_row;

    ORA_EXCEL.set_cell_value('A', rec."Дата");
    ORA_EXCEL.set_cell_format('A', 'DD.MM.YYYY HH:M');
    ORA_EXCEL.set_cell_align_left('A');

    ORA_EXCEL.set_cell_value('B', rec."Узел");
    ORA_EXCEL.set_cell_bold('B');

    ORA_EXCEL.set_cell_value('C', rec."Адрес");
    ORA_EXCEL.set_cell_bold('C');

    ORA_EXCEL.set_cell_value('D', rec.T5);
    ORA_EXCEL.set_cell_align_left('D');

    ORA_EXCEL.set_cell_value('E', rec."Ответственный");

  END LOOP;
  
  
/* ============================================================ */
  
  ORA_EXCEL.save_to_file('EXPORT_DIR', 'Hours_' || to_char(sysdate,'YYMMDD_hh24MISS') || '.xlsx');

END;
/

