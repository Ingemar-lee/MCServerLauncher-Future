﻿using System.Text;
using MCServerLauncher.Daemon.Helpers;
using MCServerLauncher.Daemon.Remote;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MCServerLauncher.Daemon.Utils;
using Newtonsoft.Json.Serialization;

namespace MCServerLauncher.Daemon
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MCServerLauncher.Daemon");
            BasicUtils.InitApp();
            TestJavaScanner();
            Server.Start();
        }

        public static async void TestJavaScanner()
        {
            JavaScanner Scanner = new();
            await Scanner.ScanJava();
        }

        public static void WriteTestJavaInfo()
        {
            Console.WriteLine(
                new JavaScanner.JavaInfo
                {
                    Architecture = "x64",
                    Path = "tmp",
                    Version = "8"
                }
            );
        }

        public static async void TestCreateInstance()
        {
            InstanceManager Manager = new();
            JObject InstanceConfig = new()
            {
                ["instanceType"] = "MinecraftJavaServer",
                ["instanceCoreFilePath"] =
                    "E:\\Desktop\\MCSL2-2.2.5.1-Windows-x64\\MCSL2\\Downloads\\Arclight-Whisper-forge-1.0.3.jar",
                ["instanceJavaRuntimePath"] = "C:\\Program Files\\Java\\jre1.8.0_291\\bin\\java.exe",
                ["instanceJvmMinimumMemory"] = 1024,
                ["instanceJvmMaximumMemory"] = 2048,
                ["instanceJvmArguments"] = new JArray
                {
                    "-XX:+UseG1GC",
                    "-XX:MaxGCPauseMillis=200",
                    "-XX:+UnlockExperimentalVMOptions",
                    "-XX:G1NewSizePercent=20",
                    "-XX:G1ReservePercent=20",
                    "-XX:G1HeapRegionSize=32M",
                    "-XX:G1HeapWastePercent=5",
                    "-XX:G1MixedGCCountTarget=4",
                    "-XX:InitiatingHeapOccupancyPercent=15",
                    "-XX:G1MixedGCLiveThresholdPercent=90",
                    "-XX:G1RSetUpdatingPauseTimePercent=5",
                    "-XX:SurvivorRatio=32",
                    "-XX:+PerfDisableSharedMem",
                    "-XX:MaxTenuringThreshold=1",
                    "-Dusing.aikars.flags=https://mcflags.emc.gs",
                    "-Daikars.new.flags=true"
                },
                ["instanceName"] = "TestInstance"
            };
            Console.WriteLine(JsonConvert.SerializeObject(InstanceConfig, Formatting.Indented));
            await Manager.CreateInstance(InstanceConfig);
        }
    }
}