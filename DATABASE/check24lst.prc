create or replace procedure check24lst
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst('54','142','23','32');/*
                               '142',
                               '23', -- 784
                               '32'
                               );*/
BEGIN
     FOR x IN
            node_name.FIRST ..
            node_name.LAST
              LOOP
                counters.check24(node_name(x));
              END LOOP;
END;
/

