# DrinkStore(Asp.Net Core 1.1)
## Running environment
- Windows
- Visual Studio 2017
- SQL Server(localDB)
You should run this app first time to generate schema in SQL Server.Then you open MSSM to add model data.
  - Copy it and paste to Styles table:`1 岩盐芝士系列
2	醇香心选系列
3	清新柠檬系列
4	多口感特调系列
5	茶拿铁系列
6	雪乐冰系列`
  - Copy it and paste to Drinks table:`1 2017-08-27 10:42:49.5840893	~/Images/yyzs-yyzslc.jpg	9.80	1	岩盐芝士绿茶
2	2017-08-27 10:47:29.3615905	/images/yyzs-yyzsmxhc.jpg	9.70	1	岩盐芝士密香红茶
3	2017-08-27 10:47:52.0212698	/images/yyzs-yyzsnc.jpg	9.60	1	岩盐芝士奶茶
4	2017-08-27 10:48:24.7692629	/images/yyzs-yyzskk.jpg	8.88	1	岩盐芝士可可
5	2017-08-27 10:49:07.3053453	/images/cxxx-oreoqqnc.jpg	9.70	2	OREO曲奇奶茶
6	2017-08-27 10:49:48.3383782	/images/cxxx-dmgbdnc.jpg	10.00	2	大满贯布丁奶茶
7	2017-08-27 10:50:46.4293103	/images/cxxx-yyxwznc.jpg	12.00	2	芋圆小丸子奶茶
8	2017-08-27 10:51:55.4478775	/images/qxnm-szxnmxhc.jpg	8.20	3	手榨香柠蜜香红茶
9	2017-08-27 10:52:35.7221660	/images/qxnm-szxnmxlv.jpg	8.20	3	手榨香柠茉香绿茶
10	2017-08-27 10:53:13.1830027	/images/qxnm-lmbd.jpg	9.80	3	柠檬冰冻
11	2017-08-27 10:53:43.0479854	/images/qxnm-nmbld.jpg	8.30	3	柠檬菠萝冻
12	2017-08-27 10:54:24.8027319	/images/dkgtt-bdkk.jpg	12.00	4	布丁可可
13	2017-08-27 10:55:00.2425320	/images/dkgtt-blqgfwhc.jpg	13.00	4	菠萝Q果风味红茶
14	2017-08-27 10:55:46.9730474	/images/dkgtt-mcqgfwhc.jpg	13.00	4	芒橙Q果风味红茶
15	2017-08-27 10:56:52.5045055	/images/cnt-hcnt.jpg	9.90	5	红豆茶拿铁
16	2017-08-27 10:57:35.0481916	/images/cnt-bdcnt.jpg	12.00	5	布丁茶拿铁
17	2017-08-27 10:58:25.1361651	/images/xlb-hdmlxlb.jpg	14.00	6	红豆抹绿雪乐冰
18	2017-08-27 10:58:55.9851122	/images/xlb-nmydxlb.jpg	14.20	6	柠檬优多雪乐冰`
## Admin port
You can manage your drink data in the admin port if the user is admin which is in the appsettings.json.
