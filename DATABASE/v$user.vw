create or replace force view v$user as
select machine,terminal,program,osuser
    from sys.v_$session where audsid=userenv('SESSIONID');

