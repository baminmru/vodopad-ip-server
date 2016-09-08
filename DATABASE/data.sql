prompt PL/SQL Developer import file
prompt Created on 29 Март 2016 г. by bami
set feedback off
set define off
prompt Disabling triggers for COLORS...
alter table COLORS disable all triggers;
prompt Disabling triggers for DEVCLASSES...
alter table DEVCLASSES disable all triggers;
prompt Disabling triggers for DATATYPE...
alter table DATATYPE disable all triggers;
prompt Disabling triggers for EDIZM...
alter table EDIZM disable all triggers;
prompt Disabling triggers for MODEMS...
alter table MODEMS disable all triggers;
prompt Disabling triggers for PARAMTYPE...
alter table PARAMTYPE disable all triggers;
prompt Disabling triggers for PIPETYPE...
alter table PIPETYPE disable all triggers;
prompt Disabling foreign key constraints for DATATYPE...
alter table DATATYPE disable constraint DATATYPE_CLASS;
prompt Disabling foreign key constraints for PARAMTYPE...
alter table PARAMTYPE disable constraint PARAMTYPE_CLASS;
prompt Deleting PIPETYPE...
delete from PIPETYPE;
commit;
prompt Deleting PARAMTYPE...
delete from PARAMTYPE;
commit;
prompt Deleting MODEMS...
delete from MODEMS;
commit;
prompt Deleting EDIZM...
delete from EDIZM;
commit;
prompt Deleting DATATYPE...
delete from DATATYPE;
commit;
prompt Deleting DEVCLASSES...
delete from DEVCLASSES;
commit;
prompt Deleting COLORS...
delete from COLORS;
commit;
prompt Loading COLORS...
insert into COLORS (colorid, color)
values (2, 15792383);
insert into COLORS (colorid, color)
values (3, 16444375);
insert into COLORS (colorid, color)
values (4, 16773083);
insert into COLORS (colorid, color)
values (5, 15654860);
insert into COLORS (colorid, color)
values (6, 13484208);
insert into COLORS (colorid, color)
values (7, 9143160);
insert into COLORS (colorid, color)
values (8, 8388564);
insert into COLORS (colorid, color)
values (9, 7794374);
insert into COLORS (colorid, color)
values (10, 6737322);
insert into COLORS (colorid, color)
values (11, 4557684);
insert into COLORS (colorid, color)
values (12, 15794175);
insert into COLORS (colorid, color)
values (13, 14741230);
insert into COLORS (colorid, color)
values (14, 12701133);
insert into COLORS (colorid, color)
values (15, 8620939);
insert into COLORS (colorid, color)
values (16, 16119260);
insert into COLORS (colorid, color)
values (17, 16770244);
insert into COLORS (colorid, color)
values (18, 15652279);
insert into COLORS (colorid, color)
values (19, 13481886);
insert into COLORS (colorid, color)
values (20, 9141611);
insert into COLORS (colorid, color)
values (21, 0);
insert into COLORS (colorid, color)
values (22, 16772045);
insert into COLORS (colorid, color)
values (23, 255);
insert into COLORS (colorid, color)
values (24, 238);
insert into COLORS (colorid, color)
values (25, 205);
insert into COLORS (colorid, color)
values (26, 139);
insert into COLORS (colorid, color)
values (27, 9055202);
insert into COLORS (colorid, color)
values (28, 10824234);
insert into COLORS (colorid, color)
values (29, 16728128);
insert into COLORS (colorid, color)
values (30, 15612731);
insert into COLORS (colorid, color)
values (31, 13447987);
insert into COLORS (colorid, color)
values (32, 9118499);
insert into COLORS (colorid, color)
values (33, 14596231);
insert into COLORS (colorid, color)
values (34, 16765851);
insert into COLORS (colorid, color)
values (35, 15648145);
insert into COLORS (colorid, color)
values (36, 13478525);
insert into COLORS (colorid, color)
values (37, 9139029);
insert into COLORS (colorid, color)
values (38, 6266528);
insert into COLORS (colorid, color)
values (39, 10024447);
insert into COLORS (colorid, color)
values (40, 9364974);
insert into COLORS (colorid, color)
values (41, 8046029);
insert into COLORS (colorid, color)
values (42, 5473931);
insert into COLORS (colorid, color)
values (43, 8388352);
insert into COLORS (colorid, color)
values (44, 7794176);
insert into COLORS (colorid, color)
values (45, 6737152);
insert into COLORS (colorid, color)
values (46, 4557568);
insert into COLORS (colorid, color)
values (47, 13789470);
insert into COLORS (colorid, color)
values (48, 16744228);
insert into COLORS (colorid, color)
values (49, 15627809);
insert into COLORS (colorid, color)
values (50, 13461021);
insert into COLORS (colorid, color)
values (51, 9127187);
insert into COLORS (colorid, color)
values (52, 16744272);
insert into COLORS (colorid, color)
values (53, 16740950);
insert into COLORS (colorid, color)
values (54, 15624784);
insert into COLORS (colorid, color)
values (55, 13458245);
insert into COLORS (colorid, color)
values (56, 9125423);
insert into COLORS (colorid, color)
values (57, 6591981);
insert into COLORS (colorid, color)
values (58, 16775388);
insert into COLORS (colorid, color)
values (59, 15657165);
insert into COLORS (colorid, color)
values (60, 13486257);
insert into COLORS (colorid, color)
values (61, 9144440);
insert into COLORS (colorid, color)
values (62, 65535);
insert into COLORS (colorid, color)
values (63, 61166);
insert into COLORS (colorid, color)
values (64, 52685);
insert into COLORS (colorid, color)
values (65, 35723);
insert into COLORS (colorid, color)
values (66, 139);
insert into COLORS (colorid, color)
values (67, 35723);
insert into COLORS (colorid, color)
values (68, 12092939);
insert into COLORS (colorid, color)
values (69, 16759055);
insert into COLORS (colorid, color)
values (70, 15641870);
insert into COLORS (colorid, color)
values (71, 13473036);
insert into COLORS (colorid, color)
values (72, 9135368);
insert into COLORS (colorid, color)
values (73, 25600);
insert into COLORS (colorid, color)
values (74, 11119017);
insert into COLORS (colorid, color)
values (75, 12433259);
insert into COLORS (colorid, color)
values (76, 9109643);
insert into COLORS (colorid, color)
values (77, 5597999);
insert into COLORS (colorid, color)
values (78, 13303664);
insert into COLORS (colorid, color)
values (79, 12381800);
insert into COLORS (colorid, color)
values (80, 10669402);
insert into COLORS (colorid, color)
values (81, 7244605);
insert into COLORS (colorid, color)
values (82, 16747520);
insert into COLORS (colorid, color)
values (83, 16744192);
insert into COLORS (colorid, color)
values (84, 15627776);
insert into COLORS (colorid, color)
values (85, 13460992);
insert into COLORS (colorid, color)
values (86, 9127168);
insert into COLORS (colorid, color)
values (87, 10040012);
insert into COLORS (colorid, color)
values (88, 12533503);
insert into COLORS (colorid, color)
values (89, 11680494);
insert into COLORS (colorid, color)
values (90, 10105549);
insert into COLORS (colorid, color)
values (91, 6824587);
insert into COLORS (colorid, color)
values (92, 9109504);
insert into COLORS (colorid, color)
values (93, 15308410);
insert into COLORS (colorid, color)
values (94, 9419919);
insert into COLORS (colorid, color)
values (95, 12713921);
insert into COLORS (colorid, color)
values (96, 11857588);
insert into COLORS (colorid, color)
values (97, 10210715);
insert into COLORS (colorid, color)
values (98, 6916969);
insert into COLORS (colorid, color)
values (99, 4734347);
insert into COLORS (colorid, color)
values (100, 3100495);
insert into COLORS (colorid, color)
values (101, 9961471);
commit;
prompt 100 records committed...
insert into COLORS (colorid, color)
values (102, 9301742);
insert into COLORS (colorid, color)
values (103, 7982541);
insert into COLORS (colorid, color)
values (104, 5409675);
insert into COLORS (colorid, color)
values (105, 52945);
insert into COLORS (colorid, color)
values (106, 9699539);
insert into COLORS (colorid, color)
values (107, 16716947);
insert into COLORS (colorid, color)
values (108, 15602313);
insert into COLORS (colorid, color)
values (109, 13439094);
insert into COLORS (colorid, color)
values (110, 9112144);
insert into COLORS (colorid, color)
values (111, 49151);
insert into COLORS (colorid, color)
values (112, 45806);
insert into COLORS (colorid, color)
values (113, 39629);
insert into COLORS (colorid, color)
values (114, 26763);
insert into COLORS (colorid, color)
values (115, 6908265);
insert into COLORS (colorid, color)
values (116, 2003199);
insert into COLORS (colorid, color)
values (117, 1869550);
insert into COLORS (colorid, color)
values (118, 1602765);
insert into COLORS (colorid, color)
values (119, 1068683);
insert into COLORS (colorid, color)
values (120, 11674146);
insert into COLORS (colorid, color)
values (121, 16724016);
insert into COLORS (colorid, color)
values (122, 15608876);
insert into COLORS (colorid, color)
values (123, 13444646);
insert into COLORS (colorid, color)
values (124, 9116186);
insert into COLORS (colorid, color)
values (125, 16775920);
insert into COLORS (colorid, color)
values (126, 2263842);
insert into COLORS (colorid, color)
values (127, 14474460);
insert into COLORS (colorid, color)
values (128, 16316671);
insert into COLORS (colorid, color)
values (129, 16766720);
insert into COLORS (colorid, color)
values (130, 15649024);
insert into COLORS (colorid, color)
values (131, 13479168);
insert into COLORS (colorid, color)
values (132, 9139456);
insert into COLORS (colorid, color)
values (133, 14329120);
insert into COLORS (colorid, color)
values (134, 16761125);
insert into COLORS (colorid, color)
values (135, 15643682);
insert into COLORS (colorid, color)
values (136, 13474589);
insert into COLORS (colorid, color)
values (137, 9136404);
insert into COLORS (colorid, color)
values (138, 65280);
insert into COLORS (colorid, color)
values (139, 60928);
insert into COLORS (colorid, color)
values (140, 52480);
insert into COLORS (colorid, color)
values (141, 35584);
insert into COLORS (colorid, color)
values (142, 11403055);
insert into COLORS (colorid, color)
values (143, 12500670);
insert into COLORS (colorid, color)
values (144, 1842204);
insert into COLORS (colorid, color)
values (145, 3552822);
insert into COLORS (colorid, color)
values (146, 5197647);
insert into COLORS (colorid, color)
values (147, 6908265);
insert into COLORS (colorid, color)
values (148, 8553090);
insert into COLORS (colorid, color)
values (149, 10263708);
insert into COLORS (colorid, color)
values (150, 11908533);
insert into COLORS (colorid, color)
values (151, 13619151);
insert into COLORS (colorid, color)
values (152, 15263976);
insert into COLORS (colorid, color)
values (153, 15794160);
insert into COLORS (colorid, color)
values (154, 14741216);
insert into COLORS (colorid, color)
values (155, 12701121);
insert into COLORS (colorid, color)
values (156, 8620931);
insert into COLORS (colorid, color)
values (157, 16738740);
insert into COLORS (colorid, color)
values (158, 16740020);
insert into COLORS (colorid, color)
values (159, 15624871);
insert into COLORS (colorid, color)
values (160, 13459600);
insert into COLORS (colorid, color)
values (161, 9124450);
insert into COLORS (colorid, color)
values (162, 13458524);
insert into COLORS (colorid, color)
values (163, 16738922);
insert into COLORS (colorid, color)
values (164, 15623011);
insert into COLORS (colorid, color)
values (165, 13456725);
insert into COLORS (colorid, color)
values (166, 9124410);
insert into COLORS (colorid, color)
values (167, 16777200);
insert into COLORS (colorid, color)
values (168, 15658720);
insert into COLORS (colorid, color)
values (169, 13487553);
insert into COLORS (colorid, color)
values (170, 9145219);
insert into COLORS (colorid, color)
values (171, 16774799);
insert into COLORS (colorid, color)
values (172, 15656581);
insert into COLORS (colorid, color)
values (173, 13485683);
insert into COLORS (colorid, color)
values (174, 9143886);
insert into COLORS (colorid, color)
values (175, 15132410);
insert into COLORS (colorid, color)
values (176, 16773365);
insert into COLORS (colorid, color)
values (177, 15655141);
insert into COLORS (colorid, color)
values (178, 13484485);
insert into COLORS (colorid, color)
values (179, 9143174);
insert into COLORS (colorid, color)
values (180, 8190976);
insert into COLORS (colorid, color)
values (181, 16775885);
insert into COLORS (colorid, color)
values (182, 15657407);
insert into COLORS (colorid, color)
values (183, 13486501);
insert into COLORS (colorid, color)
values (184, 9144688);
insert into COLORS (colorid, color)
values (185, 11393254);
insert into COLORS (colorid, color)
values (186, 12578815);
insert into COLORS (colorid, color)
values (187, 11722734);
insert into COLORS (colorid, color)
values (188, 10141901);
insert into COLORS (colorid, color)
values (189, 6849419);
insert into COLORS (colorid, color)
values (190, 15761536);
insert into COLORS (colorid, color)
values (191, 14745599);
insert into COLORS (colorid, color)
values (192, 13758190);
insert into COLORS (colorid, color)
values (193, 11849165);
insert into COLORS (colorid, color)
values (194, 8031115);
insert into COLORS (colorid, color)
values (195, 15654274);
insert into COLORS (colorid, color)
values (196, 16772235);
insert into COLORS (colorid, color)
values (197, 15654018);
insert into COLORS (colorid, color)
values (198, 13483632);
insert into COLORS (colorid, color)
values (199, 9142604);
insert into COLORS (colorid, color)
values (200, 16448210);
insert into COLORS (colorid, color)
values (201, 9498256);
commit;
prompt 200 records committed...
insert into COLORS (colorid, color)
values (202, 13882323);
insert into COLORS (colorid, color)
values (203, 16758465);
insert into COLORS (colorid, color)
values (204, 16756409);
insert into COLORS (colorid, color)
values (205, 15639213);
insert into COLORS (colorid, color)
values (206, 13470869);
insert into COLORS (colorid, color)
values (207, 9133925);
insert into COLORS (colorid, color)
values (208, 16752762);
insert into COLORS (colorid, color)
values (209, 15635826);
insert into COLORS (colorid, color)
values (210, 13468002);
insert into COLORS (colorid, color)
values (211, 9131842);
insert into COLORS (colorid, color)
values (212, 2142890);
insert into COLORS (colorid, color)
values (213, 8900346);
insert into COLORS (colorid, color)
values (214, 11592447);
insert into COLORS (colorid, color)
values (215, 10802158);
insert into COLORS (colorid, color)
values (216, 9287373);
insert into COLORS (colorid, color)
values (217, 6323083);
insert into COLORS (colorid, color)
values (218, 8679679);
insert into COLORS (colorid, color)
values (219, 7833753);
insert into COLORS (colorid, color)
values (220, 11584734);
insert into COLORS (colorid, color)
values (221, 13296127);
insert into COLORS (colorid, color)
values (222, 12374766);
insert into COLORS (colorid, color)
values (223, 10663373);
insert into COLORS (colorid, color)
values (224, 7240587);
insert into COLORS (colorid, color)
values (225, 16777184);
insert into COLORS (colorid, color)
values (226, 15658705);
insert into COLORS (colorid, color)
values (227, 13487540);
insert into COLORS (colorid, color)
values (228, 9145210);
insert into COLORS (colorid, color)
values (229, 3329330);
insert into COLORS (colorid, color)
values (230, 16445670);
insert into COLORS (colorid, color)
values (231, 16711935);
insert into COLORS (colorid, color)
values (232, 15597806);
insert into COLORS (colorid, color)
values (233, 13435085);
insert into COLORS (colorid, color)
values (234, 9109643);
insert into COLORS (colorid, color)
values (235, 11546720);
insert into COLORS (colorid, color)
values (236, 16725171);
insert into COLORS (colorid, color)
values (237, 15610023);
insert into COLORS (colorid, color)
values (238, 13445520);
insert into COLORS (colorid, color)
values (239, 9116770);
insert into COLORS (colorid, color)
values (240, 6737322);
insert into COLORS (colorid, color)
values (241, 205);
insert into COLORS (colorid, color)
values (242, 12211667);
insert into COLORS (colorid, color)
values (243, 14706431);
insert into COLORS (colorid, color)
values (244, 13721582);
insert into COLORS (colorid, color)
values (245, 11817677);
insert into COLORS (colorid, color)
values (246, 8009611);
insert into COLORS (colorid, color)
values (247, 9662683);
insert into COLORS (colorid, color)
values (248, 11240191);
insert into COLORS (colorid, color)
values (249, 10451438);
insert into COLORS (colorid, color)
values (250, 9005261);
insert into COLORS (colorid, color)
values (251, 6113163);
insert into COLORS (colorid, color)
values (252, 3978097);
insert into COLORS (colorid, color)
values (253, 8087790);
insert into COLORS (colorid, color)
values (254, 4772300);
insert into COLORS (colorid, color)
values (255, 13047173);
insert into COLORS (colorid, color)
values (256, 64154);
insert into COLORS (colorid, color)
values (257, 1644912);
insert into COLORS (colorid, color)
values (258, 16121850);
insert into COLORS (colorid, color)
values (259, 16770273);
insert into COLORS (colorid, color)
values (260, 15652306);
insert into COLORS (colorid, color)
values (261, 13481909);
insert into COLORS (colorid, color)
values (262, 9141627);
insert into COLORS (colorid, color)
values (263, 16770229);
insert into COLORS (colorid, color)
values (264, 16768685);
insert into COLORS (colorid, color)
values (265, 15650721);
insert into COLORS (colorid, color)
values (266, 13480843);
insert into COLORS (colorid, color)
values (267, 9140574);
insert into COLORS (colorid, color)
values (268, 128);
insert into COLORS (colorid, color)
values (269, 16643558);
insert into COLORS (colorid, color)
values (270, 7048739);
insert into COLORS (colorid, color)
values (271, 12648254);
insert into COLORS (colorid, color)
values (272, 11791930);
insert into COLORS (colorid, color)
values (273, 10145074);
insert into COLORS (colorid, color)
values (274, 6916898);
insert into COLORS (colorid, color)
values (275, 16753920);
insert into COLORS (colorid, color)
values (276, 15636992);
insert into COLORS (colorid, color)
values (277, 13468928);
insert into COLORS (colorid, color)
values (278, 9132544);
insert into COLORS (colorid, color)
values (279, 16729344);
insert into COLORS (colorid, color)
values (280, 15613952);
insert into COLORS (colorid, color)
values (281, 13448960);
insert into COLORS (colorid, color)
values (282, 9118976);
insert into COLORS (colorid, color)
values (283, 14315734);
insert into COLORS (colorid, color)
values (284, 16745466);
insert into COLORS (colorid, color)
values (285, 15629033);
insert into COLORS (colorid, color)
values (286, 13461961);
insert into COLORS (colorid, color)
values (287, 9127817);
insert into COLORS (colorid, color)
values (288, 15657130);
insert into COLORS (colorid, color)
values (289, 10025880);
insert into COLORS (colorid, color)
values (290, 10157978);
insert into COLORS (colorid, color)
values (291, 9498256);
insert into COLORS (colorid, color)
values (292, 8179068);
insert into COLORS (colorid, color)
values (293, 5540692);
insert into COLORS (colorid, color)
values (294, 11529966);
insert into COLORS (colorid, color)
values (295, 12320767);
insert into COLORS (colorid, color)
values (296, 9883085);
insert into COLORS (colorid, color)
values (297, 6720395);
insert into COLORS (colorid, color)
values (298, 14381203);
insert into COLORS (colorid, color)
values (299, 16745131);
insert into COLORS (colorid, color)
values (300, 15628703);
insert into COLORS (colorid, color)
values (301, 13461641);
commit;
prompt 300 records committed...
insert into COLORS (colorid, color)
values (302, 9127773);
insert into COLORS (colorid, color)
values (303, 16773077);
insert into COLORS (colorid, color)
values (304, 16767673);
insert into COLORS (colorid, color)
values (305, 15649709);
insert into COLORS (colorid, color)
values (306, 13479829);
insert into COLORS (colorid, color)
values (307, 9140069);
insert into COLORS (colorid, color)
values (308, 13468991);
insert into COLORS (colorid, color)
values (309, 16761035);
insert into COLORS (colorid, color)
values (310, 16758213);
insert into COLORS (colorid, color)
values (311, 15641016);
insert into COLORS (colorid, color)
values (312, 13472158);
insert into COLORS (colorid, color)
values (313, 9134956);
insert into COLORS (colorid, color)
values (314, 14524637);
insert into COLORS (colorid, color)
values (315, 16759807);
insert into COLORS (colorid, color)
values (316, 15642350);
insert into COLORS (colorid, color)
values (317, 13473485);
insert into COLORS (colorid, color)
values (318, 9135755);
insert into COLORS (colorid, color)
values (319, 11591910);
insert into COLORS (colorid, color)
values (320, 10494192);
insert into COLORS (colorid, color)
values (321, 10170623);
insert into COLORS (colorid, color)
values (322, 9514222);
insert into COLORS (colorid, color)
values (323, 8201933);
insert into COLORS (colorid, color)
values (324, 5577355);
insert into COLORS (colorid, color)
values (325, 16711680);
insert into COLORS (colorid, color)
values (326, 15597568);
insert into COLORS (colorid, color)
values (327, 13434880);
insert into COLORS (colorid, color)
values (328, 9109504);
insert into COLORS (colorid, color)
values (329, 12357519);
insert into COLORS (colorid, color)
values (330, 16761281);
insert into COLORS (colorid, color)
values (331, 15643828);
insert into COLORS (colorid, color)
values (332, 13474715);
insert into COLORS (colorid, color)
values (333, 9136489);
insert into COLORS (colorid, color)
values (334, 4286945);
insert into COLORS (colorid, color)
values (335, 4749055);
insert into COLORS (colorid, color)
values (336, 4419310);
insert into COLORS (colorid, color)
values (337, 3825613);
insert into COLORS (colorid, color)
values (338, 2572427);
insert into COLORS (colorid, color)
values (339, 9127187);
insert into COLORS (colorid, color)
values (340, 16416882);
insert into COLORS (colorid, color)
values (341, 16747625);
insert into COLORS (colorid, color)
values (342, 15630946);
insert into COLORS (colorid, color)
values (343, 13463636);
insert into COLORS (colorid, color)
values (344, 9129017);
insert into COLORS (colorid, color)
values (345, 16032864);
insert into COLORS (colorid, color)
values (346, 3050327);
insert into COLORS (colorid, color)
values (347, 5570463);
insert into COLORS (colorid, color)
values (348, 5172884);
insert into COLORS (colorid, color)
values (349, 4443520);
insert into COLORS (colorid, color)
values (350, 3050327);
insert into COLORS (colorid, color)
values (351, 16774638);
insert into COLORS (colorid, color)
values (352, 15656414);
insert into COLORS (colorid, color)
values (353, 13485503);
insert into COLORS (colorid, color)
values (354, 9143938);
insert into COLORS (colorid, color)
values (355, 10506797);
insert into COLORS (colorid, color)
values (356, 16745031);
insert into COLORS (colorid, color)
values (357, 15628610);
insert into COLORS (colorid, color)
values (358, 13461561);
insert into COLORS (colorid, color)
values (359, 9127718);
insert into COLORS (colorid, color)
values (360, 8900331);
insert into COLORS (colorid, color)
values (361, 8900351);
insert into COLORS (colorid, color)
values (362, 8306926);
insert into COLORS (colorid, color)
values (363, 7120589);
insert into COLORS (colorid, color)
values (364, 4878475);
insert into COLORS (colorid, color)
values (365, 6970061);
insert into COLORS (colorid, color)
values (366, 8613887);
insert into COLORS (colorid, color)
values (367, 8021998);
insert into COLORS (colorid, color)
values (368, 6904269);
insert into COLORS (colorid, color)
values (369, 4668555);
insert into COLORS (colorid, color)
values (370, 7372944);
insert into COLORS (colorid, color)
values (371, 13034239);
insert into COLORS (colorid, color)
values (372, 12178414);
insert into COLORS (colorid, color)
values (373, 10467021);
insert into COLORS (colorid, color)
values (374, 7109515);
insert into COLORS (colorid, color)
values (375, 16775930);
insert into COLORS (colorid, color)
values (376, 15657449);
insert into COLORS (colorid, color)
values (377, 13486537);
insert into COLORS (colorid, color)
values (378, 9144713);
insert into COLORS (colorid, color)
values (379, 65407);
insert into COLORS (colorid, color)
values (380, 61046);
insert into COLORS (colorid, color)
values (381, 52582);
insert into COLORS (colorid, color)
values (382, 35653);
insert into COLORS (colorid, color)
values (383, 4620980);
insert into COLORS (colorid, color)
values (384, 6535423);
insert into COLORS (colorid, color)
values (385, 6073582);
insert into COLORS (colorid, color)
values (386, 5215437);
insert into COLORS (colorid, color)
values (387, 3564683);
insert into COLORS (colorid, color)
values (388, 13808780);
insert into COLORS (colorid, color)
values (389, 16753999);
insert into COLORS (colorid, color)
values (390, 15637065);
insert into COLORS (colorid, color)
values (391, 13468991);
insert into COLORS (colorid, color)
values (392, 9132587);
insert into COLORS (colorid, color)
values (393, 14204888);
insert into COLORS (colorid, color)
values (394, 16769535);
insert into COLORS (colorid, color)
values (395, 15651566);
insert into COLORS (colorid, color)
values (396, 13481421);
insert into COLORS (colorid, color)
values (397, 9141131);
insert into COLORS (colorid, color)
values (398, 16737095);
insert into COLORS (colorid, color)
values (399, 15621186);
insert into COLORS (colorid, color)
values (400, 13455161);
insert into COLORS (colorid, color)
values (401, 9123366);
commit;
prompt 400 records committed...
insert into COLORS (colorid, color)
values (402, 4251856);
insert into COLORS (colorid, color)
values (403, 62975);
insert into COLORS (colorid, color)
values (404, 58862);
insert into COLORS (colorid, color)
values (405, 50637);
insert into COLORS (colorid, color)
values (406, 34443);
insert into COLORS (colorid, color)
values (407, 15631086);
insert into COLORS (colorid, color)
values (408, 13639824);
insert into COLORS (colorid, color)
values (409, 16727702);
insert into COLORS (colorid, color)
values (410, 15612556);
insert into COLORS (colorid, color)
values (411, 13447800);
insert into COLORS (colorid, color)
values (412, 9118290);
insert into COLORS (colorid, color)
values (413, 16113331);
insert into COLORS (colorid, color)
values (414, 16771002);
insert into COLORS (colorid, color)
values (415, 15653038);
insert into COLORS (colorid, color)
values (416, 13482646);
insert into COLORS (colorid, color)
values (417, 9141862);
insert into COLORS (colorid, color)
values (418, 16777215);
insert into COLORS (colorid, color)
values (419, 16119285);
insert into COLORS (colorid, color)
values (420, 16776960);
insert into COLORS (colorid, color)
values (421, 15658496);
insert into COLORS (colorid, color)
values (422, 13487360);
insert into COLORS (colorid, color)
values (423, 9145088);
insert into COLORS (colorid, color)
values (424, 10145074);
commit;
prompt 423 records loaded
prompt Loading DEVCLASSES...
insert into DEVCLASSES (id_class, classname)
values (1, 'ТЕПЛОСЧЕТЧИК');
commit;
prompt 1 records loaded
prompt Loading DATATYPE...
insert into DATATYPE (id_class, id_type, ctype)
values (1, 10, '941_s_float');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 11, '941_s_TO');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 12, '941_s_DO');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 13, '941_s_DS');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 14, '941_s_DW');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 1, 'INTEGER');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 2, 'LONG');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 3, 'SINGLE');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 4, 'DOUBLE');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 5, 'DATE');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 6, 'STRING');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 7, 'BYTE');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 8, 'VARIANT');
insert into DATATYPE (id_class, id_type, ctype)
values (1, 9, 'HC');
commit;
prompt 14 records loaded
prompt Loading EDIZM...
insert into EDIZM (edizm_id, name, possibleparam, edizm_base, multipicator, divider)
values (2, '°С', 't1,t2,t3,t4,t5,t6,dt12,dt34,dt56,tcool,thot,tair1,tair2', null, 1, 1);
insert into EDIZM (edizm_id, name, possibleparam, edizm_base, multipicator, divider)
values (3, 'м3', 'v1,v2,v3,v4,v5,v6,dv12,dv34,dv45,g1,g2,g3,g4,g5,g6', null, 1, 1);
insert into EDIZM (edizm_id, name, possibleparam, edizm_base, multipicator, divider)
values (4, 'т.', 'm1,m2,m3,m4,m5,m6,dm12,dm34,dm45,g1,g2,g3,g4,g5,g6', null, 1, 1);
insert into EDIZM (edizm_id, name, possibleparam, edizm_base, multipicator, divider)
values (5, 'Кдж', 'q1,q2,q3,q4,q5,q6,dq,q1h,q2h', 8, 1, 1);
insert into EDIZM (edizm_id, name, possibleparam, edizm_base, multipicator, divider)
values (6, 'Гдж', 'q1,q2,q3,q4,q5,q6,dq,q1h,q2h', 8, 1, 1);
insert into EDIZM (edizm_id, name, possibleparam, edizm_base, multipicator, divider)
values (7, 'Ккал', 'q1,q2,q3,q4,q5,q6,dq,q1h,q2h', 8, 1, 1000);
insert into EDIZM (edizm_id, name, possibleparam, edizm_base, multipicator, divider)
values (8, 'Гкал', 'q1,q2,q3,q4,q5,q6,dq,q1h,q2h', null, 1, 1);
insert into EDIZM (edizm_id, name, possibleparam, edizm_base, multipicator, divider)
values (9, 'КПа', 'p1,p2,p3,p4,p5,p6,dp12,dp45', null, 1, 1);
insert into EDIZM (edizm_id, name, possibleparam, edizm_base, multipicator, divider)
values (10, 'МПа', 'p1,p2,p3,p4,p5,p6,dp12,dp45', null, 1, 1);
insert into EDIZM (edizm_id, name, possibleparam, edizm_base, multipicator, divider)
values (11, 'атм.', 'p1,p2,p3,p4,p5,p6,dp12,dp45', null, 1, 1);
insert into EDIZM (edizm_id, name, possibleparam, edizm_base, multipicator, divider)
values (21, 'мин', 'oktime,oktime2,errtime,errtime2,worktime', null, 1, 1);
insert into EDIZM (edizm_id, name, possibleparam, edizm_base, multipicator, divider)
values (22, 'час', 'oktime,oktime2,errtime,errtime2,worktime', 21, 1, 60);
commit;
prompt 12 records loaded
prompt Loading MODEMS...
insert into MODEMS (id_modem, cshort, cfull, cinit, dupd, cdtr, cdsr)
values (14, 'IDC', 'Модем IDC', 'at&fx4l3L0M0', '201006021529', '1', '1');
insert into MODEMS (id_modem, cshort, cfull, cinit, dupd, cdtr, cdsr)
values (16, 'MODEM USR HANDLE', 'MODEM USR HANDLE', 'at&fx4M0L0', '201603141322', '1', '1');
insert into MODEMS (id_modem, cshort, cfull, cinit, dupd, cdtr, cdsr)
values (12, 'MODEM GVC WHITE', 'MODEM GVC WHITE', 'AT&FL0M0', '201309050959', '0', '1');
insert into MODEMS (id_modem, cshort, cfull, cinit, dupd, cdtr, cdsr)
values (11, 'MODEMUSR', 'US Robotics', 'at&fx4m0l0', '201309050947', '1', '1');
insert into MODEMS (id_modem, cshort, cfull, cinit, dupd, cdtr, cdsr)
values (8, 'MODEM LANBIT', 'Модем LanBit', 'AT&FX4L0M0', '201010141750', '1', '1');
insert into MODEMS (id_modem, cshort, cfull, cinit, dupd, cdtr, cdsr)
values (9, 'MODEM GVC', 'Modem GVC', 'AT&FX4L0M0', '200910271246', '1', '1');
insert into MODEMS (id_modem, cshort, cfull, cinit, dupd, cdtr, cdsr)
values (13, 'TOSHIBA', 'toshiba', 'at&fl0m0', '201006021530', '1', '1');
insert into MODEMS (id_modem, cshort, cfull, cinit, dupd, cdtr, cdsr)
values (15, 'GVC WHITE HANDLE', 'GVC WHITE HANDLE', 'AT&F', '201403241452', '1', '1');
insert into MODEMS (id_modem, cshort, cfull, cinit, dupd, cdtr, cdsr)
values (17, 'URS56-USB', null, 'at&fl0m0', '201006021530', '1', '1');
commit;
prompt 9 records loaded
prompt Loading PARAMTYPE...
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 7, 'Дата счетчика, единицы измерения и т.д.');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 0, 'Системные');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 1, 'Мгновенные');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 2, 'Итоговые');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 3, 'Часовые Архивные');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 4, 'Суточные Архивы');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 5, 'Нештатная Ситуация');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 99, 'Неизвестный');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 10, '10???');
insert into PARAMTYPE (id_class, id_type, ctype)
values (1, 6, 'Расчетный');
commit;
prompt 10 records loaded
prompt Loading PIPETYPE...
insert into PIPETYPE (id_pipe, pipename)
values (0, 'Не определено');
insert into PIPETYPE (id_pipe, pipename)
values (1, 'Прямой трубопровод');
insert into PIPETYPE (id_pipe, pipename)
values (2, 'Обратный трубопровод');
insert into PIPETYPE (id_pipe, pipename)
values (3, 'Трубопровод подпитки');
insert into PIPETYPE (id_pipe, pipename)
values (4, 'Трубопровод конденсата');
insert into PIPETYPE (id_pipe, pipename)
values (5, 'Трубопровод ХВС');
insert into PIPETYPE (id_pipe, pipename)
values (6, 'Трубопровод ГВС');
insert into PIPETYPE (id_pipe, pipename)
values (7, 'Аварийный слив');
insert into PIPETYPE (id_pipe, pipename)
values (8, 'Прямой трубопровод системы');
insert into PIPETYPE (id_pipe, pipename)
values (9, 'Обратный трубопровод системы');
insert into PIPETYPE (id_pipe, pipename)
values (10, 'Подпиточный трубопровод системы');
insert into PIPETYPE (id_pipe, pipename)
values (11, 'Прямой трубопровод ГВС');
insert into PIPETYPE (id_pipe, pipename)
values (12, 'Обратный трубопровод ГВС');
insert into PIPETYPE (id_pipe, pipename)
values (13, 'Подпиточный трубопровод ГВС');
insert into PIPETYPE (id_pipe, pipename)
values (14, 'Паропровод');
insert into PIPETYPE (id_pipe, pipename)
values (15, 'Трубопровод особого назначения');
insert into PIPETYPE (id_pipe, pipename)
values (16, 'Прямой трубопровод 1-го контура');
insert into PIPETYPE (id_pipe, pipename)
values (17, 'Обратный трубопровод 1-го контура');
insert into PIPETYPE (id_pipe, pipename)
values (18, 'Трубопровод сброса');
commit;
prompt 19 records loaded
prompt Enabling foreign key constraints for DATATYPE...
alter table DATATYPE enable constraint DATATYPE_CLASS;
prompt Enabling foreign key constraints for PARAMTYPE...
alter table PARAMTYPE enable constraint PARAMTYPE_CLASS;
prompt Enabling triggers for COLORS...
alter table COLORS enable all triggers;
prompt Enabling triggers for DEVCLASSES...
alter table DEVCLASSES enable all triggers;
prompt Enabling triggers for DATATYPE...
alter table DATATYPE enable all triggers;
prompt Enabling triggers for EDIZM...
alter table EDIZM enable all triggers;
prompt Enabling triggers for MODEMS...
alter table MODEMS enable all triggers;
prompt Enabling triggers for PARAMTYPE...
alter table PARAMTYPE enable all triggers;
prompt Enabling triggers for PIPETYPE...
alter table PIPETYPE enable all triggers;
set feedback on
set define on
prompt Done.
