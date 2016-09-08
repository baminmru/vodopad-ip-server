create or replace procedure xml_lst_full(sdate IN varchar2, edate IN varchar2)
is
  TYPE id_lst_type IS TABLE OF NUMBER(4);
  node_id_si1spt942tv1 id_lst_type := id_lst_type (8,9,12,13,16,36,39,43,45,51,54,59,63,64,84,87,88,89,90,92,97,98,99,101,113,114,115,116,117,118,119,142,147,148,149,294,295);--'АТС 751','АТС 786','АТС 351'
  node_id_si1spt942tv2 id_lst_type := id_lst_type (151); --СПТ942 ГМУЭ
-- Список для СПТ943 (23.12.2013)
  node_id_si1spt943tv1 id_lst_type := id_lst_type (4,5,7,17,18,20,21,23,24,26,28,32,35,49,53,57,58,61,62,85,96,100,104,108,110,155,160,161,162,163,284);

  type_id NUMBER := 3;

BEGIN
   BEGIN
     FOR x IN
             node_id_si1spt942tv1.FIRST..node_id_si1spt942tv1.LAST
              LOOP
                counters.xml_si1spt942tv1( node_id_si1spt942tv1(x), type_id, sdate, edate);
              END LOOP;
   END;
-- Проверка выполнения двух циклов для разных типов ТВ и схем
   BEGIN
     FOR x IN
             node_id_si1spt942tv2.FIRST..node_id_si1spt942tv2.LAST
              LOOP
                counters.xml_si1spt942tv1( node_id_si1spt942tv2(x), type_id, sdate, edate);
              END LOOP;
   END;
-- Цикл для ТВ с СПТ943 1-й тепловой ввод
   BEGIN
     FOR x IN
             node_id_si1spt943tv1.FIRST..node_id_si1spt943tv1.LAST
              LOOP
                counters.xml_si1spt942tv1( node_id_si1spt943tv1(x), type_id, sdate, edate);
              END LOOP;
   END;

END;
/

