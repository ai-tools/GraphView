﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphView;
using GraphViewUnitTest.Gremlin;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace GraphViewUnitTest
{
    [TestClass]
    public class GraphViewMarvelTest
    {
        [TestMethod]
        public void SelectMarvelQuery1()
        {
            GraphViewConnection connection = new GraphViewConnection("https://graphview.documents.azure.com:443/",
                "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
                "GroupMatch", "MarvelTest", GraphType.GraphAPIOnly, AbstractGremlinTest.TEST_USE_REVERSE_EDGE,
                AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, null);

            GraphViewCommand graph = new GraphViewCommand(connection);
            graph.OutputFormat = OutputFormat.GraphSON;
            var results = graph.g().V().Has("weapon", "shield").As("character").Out("appeared").As("comicbook").Select("character").Next();

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void SelectMarvelQuery1b()
        {
            GraphViewConnection connection = new GraphViewConnection("https://graphview.documents.azure.com:443/",
                "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
                "GroupMatch", "MarvelTest", GraphType.GraphAPIOnly, AbstractGremlinTest.TEST_USE_REVERSE_EDGE,
                AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, null);

            GraphViewCommand cmd = new GraphViewCommand(connection);
            cmd.CommandText =
                "g.V().has('weapon','shield').as('character').out('appeared').as('comicbook').select('character').next()";
            cmd.OutputFormat = OutputFormat.GraphSON;
            var results = cmd.Execute();

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void SelectMarvelQuery1c()
        {
            GraphViewConnection connection = new GraphViewConnection("https://graphview.documents.azure.com:443/",
                "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
                "GroupMatch", "MarvelTest", GraphType.GraphAPIOnly, AbstractGremlinTest.TEST_USE_REVERSE_EDGE,
                AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, null);

            GraphViewCommand cmd = new GraphViewCommand(connection);
            cmd.CommandText =
                "g.V().has('weapon','shield').as('character').outE('appeared').next()";
            cmd.OutputFormat = OutputFormat.GraphSON;
            var results = cmd.Execute();

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void SelectMarvelQuery2()
        {
            GraphViewConnection connection = new GraphViewConnection("https://graphview.documents.azure.com:443/",
                "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
                "GroupMatch", "MarvelTest", GraphType.GraphAPIOnly, AbstractGremlinTest.TEST_USE_REVERSE_EDGE,
                AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, null);
            GraphViewCommand graph = new GraphViewCommand(connection);

            var results =
                graph.g()
                    .V()
                    .Has("weapon", "lasso")
                    .As("character")
                    .Out("appeared")
                    .As("comicbook")
                    .Select("comicbook")
                    .Next();

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void SelectMarvelQuery2b()
        {
            GraphViewConnection connection = new GraphViewConnection("https://graphview.documents.azure.com:443/",
                "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
                "GroupMatch", "MarvelTest", GraphType.GraphAPIOnly, AbstractGremlinTest.TEST_USE_REVERSE_EDGE,
                AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, null);
            GraphViewCommand graph = new GraphViewCommand(connection);
            graph.CommandText = "g.V().has('weapon', 'lasso').as('character').out('appeared').as('comicbook').select('comicbook').next()";
            graph.OutputFormat = OutputFormat.GraphSON;
            var results = graph.Execute();

            foreach (string result in results)
            {
                Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void SelectMarvelQuery3()
        {
            GraphViewConnection connection = new GraphViewConnection("https://graphview.documents.azure.com:443/",
                "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
                "GroupMatch", "MarvelTest", GraphType.GraphAPIOnly, AbstractGremlinTest.TEST_USE_REVERSE_EDGE,
                AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, null);
            GraphViewCommand graph = new GraphViewCommand(connection);
            var results = graph.g().V().Has("name", "AVF 4").In("appeared").Values("name").Next();

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void SelectMarvelQuery3b()
        {
            GraphViewConnection connection = new GraphViewConnection("https://graphview.documents.azure.com:443/",
                "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
                "GroupMatch", "MarvelTest", GraphType.GraphAPIOnly, AbstractGremlinTest.TEST_USE_REVERSE_EDGE,
                AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, null);
            GraphViewCommand graph = new GraphViewCommand(connection);
            graph.CommandText = "g.V().has('name', 'AVF 4').in('appeared').values('name').next()";
            var results = graph.Execute();

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void SelectMarvelQuery4()
        {
            GraphViewConnection connection = new GraphViewConnection("https://graphview.documents.azure.com:443/",
                "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
                "GroupMatch", "MarvelTest", GraphType.GraphAPIOnly, AbstractGremlinTest.TEST_USE_REVERSE_EDGE,
                AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, null);
            GraphViewCommand graph = new GraphViewCommand(connection);
            var results = graph.g().V().Has("name", "AVF 4").In("appeared").Has("weapon", "shield").Values("name").Next();

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void SelectMarvelQuery4b()
        {
            GraphViewConnection connection = new GraphViewConnection("https://graphview.documents.azure.com:443/",
                "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
                "GroupMatch", "MarvelTest", GraphType.GraphAPIOnly, AbstractGremlinTest.TEST_USE_REVERSE_EDGE,
                AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, null);
            GraphViewCommand graph = new GraphViewCommand(connection);
            graph.CommandText = "g.V().has('name', 'AVF 4').in('appeared').has('weapon', 'shield').values('name').next()";
            var results = graph.Execute();

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        /// <summary>
        /// Print the characters and the comic-books they appeared in where the characters had a weapon that was a shield or claws.
        /// </summary>
        [TestMethod]
        public void SelectMarvelQueryNativeAPI1()
        {
            GraphViewConnection connection = new GraphViewConnection("https://graphview.documents.azure.com:443/",
                "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
                "GroupMatch", "MarvelTest", GraphType.GraphAPIOnly, AbstractGremlinTest.TEST_USE_REVERSE_EDGE,
                AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, null);
            GraphViewCommand graph = new GraphViewCommand(connection);
            var results =
                graph.g().V()
                    .As("character")
                    .Has("weapon", Predicate.within("shield", "claws"))
                    .Out("appeared")
                    .As("comicbook")
                    .Select("character");

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void SelectMarvelQueryNativeAPI2()
        {
            GraphViewConnection connection = new GraphViewConnection("https://graphview.documents.azure.com:443/",
                "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
                "GroupMatch", "MarvelTest", GraphType.GraphAPIOnly, AbstractGremlinTest.TEST_USE_REVERSE_EDGE,
                AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, null);
            GraphViewCommand graph = new GraphViewCommand(connection);
            var results =
                graph.g().V()
                    .As("CharacterNode")
                    .Values("name")
                    .As("character")
                    .Select("CharacterNode")
                    .Has("weapon", Predicate.without("shield", "claws"))
                    .Out("appeared")
                    .Values("name")
                    .As("comicbook")
                    .Select("comicbook")
                    .Next();

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void GraphViewMarvelInsertDeleteTest()
        {
            GraphViewConnection connection = new GraphViewConnection("https://graphview.documents.azure.com:443/",
                "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
                "GroupMatch", "MarvelTest", GraphType.GraphAPIOnly, AbstractGremlinTest.TEST_USE_REVERSE_EDGE,
                AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, null);
            GraphViewCommand graph = new GraphViewCommand(connection);

            graph.g().AddV("character").Property("name", "VENUS II").Property("weapon", "shield").Next();
            graph.g().AddV("comicbook").Property("name", "AVF 4").Next();
            graph.g().V().Has("name", "VENUS II").AddE("appeared").To(graph.g().V().Has("name", "AVF 4")).Next();
            graph.g().AddV("character").Property("name", "HAWK").Property("weapon", "claws").Next();
            graph.g().V().As("v").Has("name", "HAWK").AddE("appeared").To(graph.g().V().Has("name", "AVF 4")).Next();
            graph.g().AddV("character").Property("name", "WOODGOD").Property("weapon", "lasso").Next();
            graph.g().V().As("v").Has("name", "WOODGOD").AddE("appeared").To(graph.g().V().Has("name", "AVF 4")).Next();
        }

        [TestMethod]
        public void GraphViewMarvelInsertTest()
        {
            //GraphViewConnection connection = new GraphViewConnection("https://graphview.documents.azure.com:443/",
            //    "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
            //    "GroupMatch", "PartitionTest", GraphType.GraphAPIOnly, AbstractGremlinTest.TEST_USE_REVERSE_EDGE,
            //    AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, null);

            GraphViewConnection connection = GraphViewConnection.ResetGraphAPICollection("https://graphview.documents.azure.com:443/",
              "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
              "GroupMatch", "PartitionTest", AbstractGremlinTest.TEST_USE_REVERSE_EDGE, AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, AbstractGremlinTest.TEST_PARTITION_BY_KEY);
            connection.EdgeSpillThreshold = 1;
            
            GraphViewCommand cmd = new GraphViewCommand(connection);

            cmd.CommandText = "g.addV('character').property('name', 'VENUS II').property('weapon', 'shield').next()";
            cmd.Execute();
            cmd.CommandText = "g.addV('comicbook').property('name', 'AVF 4').next()";
            cmd.Execute();
            cmd.CommandText = "g.V().has('name', 'VENUS II').addE('appeared').to(g.V().has('name', 'AVF 4')).next()";
            cmd.Execute();
            cmd.CommandText = "g.addV('character').property('name', 'HAWK').property('weapon', 'claws').next()";
            cmd.Execute();
            cmd.CommandText = "g.V().as('v').has('name', 'HAWK').addE('appeared').to(g.V().has('name', 'AVF 4')).next()";
            cmd.Execute();
            cmd.CommandText = "g.addV('character').property('name', 'WOODGOD').property('weapon', 'lasso').next()";
            cmd.Execute();
            cmd.CommandText = "g.V().as('v').has('name', 'WOODGOD').addE('appeared').to(g.V().has('name', 'AVF 4')).next()";
            cmd.Execute();
        }

        [TestMethod]
        public void GraphViewFakePartitionDataInsertTest()
        {
            GraphViewConnection connection = GraphViewConnection.ResetGraphAPICollection("https://graphview.documents.azure.com:443/",
              "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
              "GroupMatch", "PartitionTest", AbstractGremlinTest.TEST_USE_REVERSE_EDGE, AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, AbstractGremlinTest.TEST_PARTITION_BY_KEY);
            connection.EdgeSpillThreshold = 1;

            GraphViewCommand cmd = new GraphViewCommand(connection);

            cmd.CommandText = "g.addV('id', '1').property('name', '1').next()";
            cmd.Execute();
            cmd.CommandText = "g.addV('id', '2').property('name', '2').next()";
            cmd.Execute();
            cmd.CommandText = "g.V('1').addE('appeared').to(g.V('2')).next()";
            cmd.Execute();
            cmd.CommandText = "g.addV('id', '3').property('name', '3').next()";
            cmd.Execute();
            cmd.CommandText = "g.V('3').addE('appeared').to(g.V('2')).next()";
            cmd.Execute();
            cmd.CommandText = "g.addV('id', '4').property('name', '4').next()";
            cmd.Execute();
            cmd.CommandText = "g.V('4').addE('appeared').to(g.V('2')).next()";
            cmd.Execute();
            cmd.CommandText = "g.addV('id', '5').property('name', '5').next()";
            cmd.Execute();
            cmd.CommandText = "g.V('5').addE('appeared').to(g.V('2')).next()";
            cmd.Execute();


            cmd.CommandText = "g.addV('id', '11').property('name', '11').next()";
            cmd.Execute();
            cmd.CommandText = "g.addV('id', '12').property('name', '12').next()";
            cmd.Execute();
            cmd.CommandText = "g.V('11').addE('appeared').to(g.V('12')).next()";
            cmd.Execute();
            cmd.CommandText = "g.addV('id', '13').property('name', '13').next()";
            cmd.Execute();
            cmd.CommandText = "g.V('13').addE('appeared').to(g.V('12')).next()";
            cmd.Execute();
            cmd.CommandText = "g.addV('id', '14').property('name', '14').next()";
            cmd.Execute();
            cmd.CommandText = "g.V('14').addE('appeared').to(g.V('12')).next()";
            cmd.Execute();
            cmd.CommandText = "g.addV('id', '15').property('name', '15').next()";
            cmd.Execute();
            cmd.CommandText = "g.V('15').addE('appeared').to(g.V('12')).next()";
            cmd.Execute();

            cmd.CommandText = "g.addV('id', '21').property('name', '21').next()";
            cmd.Execute();
            cmd.CommandText = "g.addV('id', '22').property('name', '22').next()";
            cmd.Execute();
            cmd.CommandText = "g.V('21').addE('appeared').to(g.V('22')).next()";
            cmd.Execute();
            cmd.CommandText = "g.addV('id', '23').property('name', '23').next()";
            cmd.Execute();
            cmd.CommandText = "g.V('23').addE('appeared').to(g.V('22')).next()";
            cmd.Execute();
            cmd.CommandText = "g.addV('id', '24').property('name', '24').next()";
            cmd.Execute();
            cmd.CommandText = "g.V('24').addE('appeared').to(g.V('22')).next()";
            cmd.Execute();
            cmd.CommandText = "g.addV('id', '25').property('name', '25').next()";
            cmd.Execute();
            cmd.CommandText = "g.V('25').addE('appeared').to(g.V('22')).next()";
            cmd.Execute();

            var statistic = connection.ExecuteQuery("");
        }


        [TestMethod]
        public void GraphViewInsertWikiTalkTempNetworkDataVertex()
        {

            GraphViewConnection connection = GraphViewConnection.ResetGraphAPICollection("https://graphview.documents.azure.com:443/",
      "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
      "GroupMatch", "PartitionTest", AbstractGremlinTest.TEST_USE_REVERSE_EDGE, AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, AbstractGremlinTest.TEST_PARTITION_BY_KEY);
            connection.EdgeSpillThreshold = 1;
            GraphViewCommand cmd = new GraphViewCommand(connection);

            // Add vertex g.addV("id", 1)
            var idSet = new HashSet<String>();
            var lines = File.ReadLines("D:\\dataset\\thsinghua_dataset\\wiki\\wiki-talk-temporal-usernames.txt\\wiki-talk-temporal-usernames.txt");
            foreach (var line in lines)
            {
                var split = line.Split(' ');
                var id = split[0];
                var name = split[1];
                cmd.CommandText = "g.addV('id', '" + id + "').property('name', '" + name + "').next()";
                idSet.Add(id);
                cmd.Execute();
                Console.WriteLine(cmd.CommandText);
            }
        }

        [TestMethod]
        public void GraphViewInsertWikiTalkTempNetworkDataEdge()
        {

            GraphViewConnection connection = new GraphViewConnection("https://graphview.documents.azure.com:443/",
                "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
                "GroupMatch", "Wiki_Temp", GraphType.GraphAPIOnly, AbstractGremlinTest.TEST_USE_REVERSE_EDGE,
                AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, null);
            GraphViewCommand cmd = new GraphViewCommand(connection);
            // Add edge
            var linesE = File.ReadLines("D:\\dataset\\thsinghua_dataset\\wiki\\wiki-talk-temporal.txt\\wiki-talk-temporal.txt");
            foreach (var lineE in linesE)
            {
                var split = lineE.Split(' ');
                var src = split[0];
                var des = split[1];
                var date = split[2];
                cmd.CommandText = "g.V('" + src + "').addE('" + date + "').to(g.V('" + des + "')).next()";
                cmd.Execute();
            }
        }

        [TestMethod]
        public void GraphViewInsertCitNetworkDataVertex()
        {
            GraphViewConnection connection = GraphViewConnection.ResetGraphAPICollection("https://graphview.documents.azure.com:443/",
               "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
               "GroupMatch", "PartitionTest", AbstractGremlinTest.TEST_USE_REVERSE_EDGE, AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, AbstractGremlinTest.TEST_PARTITION_BY_KEY);
            connection.EdgeSpillThreshold = 1;
            GraphViewCommand cmd = new GraphViewCommand(connection);

            // Add vertex abstract
            var linesA = File.ReadLines("D:\\dataset\\thsinghua_dataset\\cit_network\\cit-HepTh-dates.txt\\Cit-HepTh-dates.txt");
            var verDic = new Dictionary<String, String>();
            int c = 0;
            foreach (var lineA in linesA)
            {
                if (c > 0)
                {
                    var split = lineA.Split('\t');
                    var id = split[0];
                    var date = split[1];
                    if (verDic.ContainsKey(id))
                    {
                        verDic[id] = verDic[id] + "," + date;
                    }
                    else
                    {
                        verDic.Add(id, date);
                    }
                    Console.WriteLine(cmd.CommandText);
                }
                c++;
            }

            var verDicTemp = new Dictionary<String, String>();
            foreach (var v in verDic)
            {
                verDicTemp[v.Key] = ".property('date', '" + verDic[v.Key] + "')";
            }

            verDic = verDicTemp;

            int c1 = 0;
            // Add vertex submit time
            for (int i = 1992; i <= 2003; i++)
            {
                if (c1 > 1)
                {
                    break;
                }
                c1++;
                var Dir = "D:\\dataset\\thsinghua_dataset\\cit_network\\cit-HepTh-abstracts.tar\\cit-HepTh-abstracts\\" + i;
                DirectoryInfo TheFolder = new DirectoryInfo(Dir);
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                {

                    var lineBs = File.ReadAllText(NextFile.DirectoryName + "\\" + NextFile.Name);
                    var splits1 = lineBs.Split('\\');
                    var prop = splits1[2];
                    var properties = prop.Split('\n');
                    var vid = NextFile.Name.Replace(".abs", "").ToString();


                    foreach (var p in properties)
                    {
                        if (p != "")
                        {
                            var subSplit = p.Split(':');

                            if (subSplit.Length == 2)
                            {
                                // clean the data
                                var value = subSplit[1].Replace("$", "").Replace("\"", "").Replace("'", "");
                                if (verDic.ContainsKey(vid))
                                {
                                    verDic[vid] = verDic[vid] + ".property('" + subSplit[0] + "', '" + value + "')";
                                }
                                else
                                {
                                    verDic.Add(vid, ".property('" + subSplit[0] + "', '" + value + "')");
                                }
                            }
                        }
                    }

                    // var content = splits1[4];
                    // verDic[vid] = verDic[vid] + ".property('" + "content" + "', '" + content + "')";
                }
            }

            foreach (var v in verDic)
            {
                cmd.CommandText = "g.addV('id', '" + v.Key + "')" + v.Value + ".next()";
                cmd.Execute();
                Console.WriteLine("Finish");
            }
            Console.WriteLine("Finish");
        }

        [TestMethod]
        public void GraphViewInsertCitNetworkDataEdge()
        {
            GraphViewConnection connection = GraphViewConnection.ResetGraphAPICollection("https://graphview.documents.azure.com:443/",
      "MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
      "GroupMatch", "PartitionTest", AbstractGremlinTest.TEST_USE_REVERSE_EDGE, AbstractGremlinTest.TEST_SPILLED_EDGE_THRESHOLD_VIAGRAPHAPI, AbstractGremlinTest.TEST_PARTITION_BY_KEY);
            connection.EdgeSpillThreshold = 1;
            GraphViewCommand cmd = new GraphViewCommand(connection);
            // Add edge
            int c = 1;
            var linesE = File.ReadLines("D:\\dataset\\thsinghua_dataset\\cit_network\\cit-HepTh.txt\\Cit-HepTh.txt");
            foreach (var lineE in linesE)
            {
                if (c > 4)
                {
                    var split = lineE.Split('\t');
                    var src = split[0];
                    var des = split[1];
                    var traversal = cmd.g().V(src);
                    var results1 = traversal.Next();
                    var traversal2 = cmd.g().V(des);
                    var results2 = traversal2.Next();

                    bool flag = false;
                    if (results1.Count > 0 && results2.Count > 0)
                    {
                        flag = true;
                    }

                    if (flag)
                    {
                        cmd.CommandText = "g.V('" + src + "').addE('" + DateTime.Now.Millisecond.ToString() + "').to(g.V('" + des + "')).next()";
                        cmd.Execute();
                    }
                    else
                    {
                        Console.WriteLine("Contains null vertex, Don't add Edge");
                    }
                    Console.WriteLine(cmd.CommandText);
                }
                else
                {
                    c++;
                }
            }
        }

    }
}
