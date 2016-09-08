create or replace force view v_linked_day as
select datacurr."ID",datacurr."ID_BD",datacurr."ID_DU",datacurr."ID_PTYPE",datacurr."DCALL",datacurr."DCOUNTER",datacurr."Q1",datacurr."Q2",datacurr."T1",datacurr."T2",datacurr."DT12",datacurr."T3",datacurr."T4",datacurr."T5",datacurr."DT45",datacurr."T6",datacurr."V1",datacurr."V2",datacurr."DV12",datacurr."V3",datacurr."V4",datacurr."V5",datacurr."DV45",datacurr."V6",datacurr."M1",datacurr."M2",datacurr."DM12",datacurr."M3",datacurr."M4",datacurr."M5",datacurr."DM45",datacurr."M6",datacurr."P1",datacurr."P2",datacurr."P3",datacurr."P4",datacurr."P5",datacurr."P6",datacurr."G1",datacurr."G2",datacurr."G3",datacurr."G4",datacurr."G5",datacurr."G6",datacurr."TCOOL",datacurr."TCE1",datacurr."TCE2",datacurr."TSUM1",datacurr."TSUM2",datacurr."Q1H",datacurr."Q2H",datacurr."V1H",datacurr."V2H",datacurr."V4H",datacurr."V5H",datacurr."ERRTIME",datacurr."ERRTIMEH",datacurr."HC",datacurr."SP",datacurr."SP_TB1",datacurr."SP_TB2",datacurr."DATECOUNTER",datacurr."DG12",datacurr."DG45",datacurr."DP12",datacurr."DP45",datacurr."UNITSR",datacurr."Q3",datacurr."Q4",datacurr."PATM",datacurr."Q5",datacurr."DQ12",datacurr."DQ45",datacurr."PXB",datacurr."DQ",datacurr."HC_1",datacurr."HC_2",datacurr."THOT",datacurr."DANS1",datacurr."DANS2",datacurr."DANS3",datacurr."DANS4",datacurr."DANS5",datacurr."DANS6",datacurr."CHECK_A",datacurr."OKTIME",datacurr."WORKTIME",datacurr."TAIR1",datacurr."TAIR2",datacurr."HC_CODE",datacurr."ERRTIME2",datacurr."OKTIME2",datacurr."Q6",datacurr."D_EQL_24",datacurr."HCRAW1",datacurr."HCRAW2",datacurr."HCRAW"
,dans."ID_BD2",dans."L_CNT",dans."DAT",dans."L_TAIR1",dans."L_T1",dans."L_T2",dans."L_T3",dans."L_T4",dans."L_T5",dans."L_T6",dans."L_G1",dans."L_G2",dans."L_G3",dans."L_G4",dans."L_G5",dans."L_G6",dans."L_V1",dans."L_V2",dans."L_V3",dans."L_V4",dans."L_V5",dans."L_V6",dans."L_Q1",dans."L_Q2",dans."L_Q3",dans."L_Q4",dans."L_P1",dans."L_P2",dans."L_P3",dans."L_P4",dans."L_P5",dans."L_P6"
  from datacurr
join bdevices on datacurr.id_bd=bdevices.ID_BD
left join v_dansd dans on dans.id_bd2=bdevices.Linked_id_bd  and dans.dat =to_char(datacurr.dcounter,'YYYY/MM/DD');
