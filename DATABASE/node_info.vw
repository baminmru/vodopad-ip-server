create or replace force view node_info as
select tb.cshort ||' ('|| td.cdevdesc ||') '|| tm.cphone ninfo,tbd.id_bd
    from devices td,bbuildings tb,bmodems tm,bdevices tbd
      where tbd.id_bu=tb.id_bu and tbd.id_dev=td.id_dev and tbd.id_bd=tm.id_bd;

