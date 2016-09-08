create or replace procedure lst_MT200si2(sdate IN varchar2, edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst(--'АТС 225',
                               --'АТС 252', -- Убрал 11.09.2008 замена ТВ на СПТ943
                               'АТС 314',--'АТС 316', -- убрал 19.12.2007
                               --'АТС 360', -- Убрал 23.01.2008 замена ТВ СПТ943
                               --'АТС 446', -- Убрал 16.10.2008 замена ТВ на СПТ943
                               --'АТС 585', -- Убрал 28.11.2013 замена ТВ на СПТ943
                               'АТС 714'
                               --'АТС 726', -- Убрал 24.01.2008 замена ТВ СПТ943
                               --'АТС 752', -- Убрал 16.10.2008 замена ТВ СПТ943
                               --'АТС 757'  -- 2014.10.07 замена на ТВ на СПТ943
                               ); -- 10 узлов
BEGIN
     FOR x IN
            node_name.FIRST ..
            node_name.LAST
              LOOP -- изменить на процедуру для МТ200DS
                counters.si2mt200(node_name(x),sdate,edate);
              END LOOP;
END;
/

