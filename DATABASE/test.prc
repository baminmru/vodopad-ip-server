create or replace procedure test
IS
 TYPE NumbersTab IS TABLE OF NUMBER;
      v_Tab1 NumbersTab := NumbersTab(-1);
      v_Primes NumbersTab := NumbersTab(1, 2, 3, 5, 7);
      v_Tab2 NumbersTab := NumbersTab();
BEGIN
     v_Tab1(1) := 12345;

         FOR v_Count IN 1..5 LOOP
             DBMS_OUTPUT.PUT(v_Primes(v_Count) || ' ');
         END LOOP;
     DBMS_OUTPUT.NEW_LINE;
END;
/

