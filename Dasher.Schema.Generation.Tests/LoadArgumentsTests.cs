﻿using Xunit;

namespace Dasher.Schema.Generation.Tests
{
    public class LoadArgumentsTests
    {


        public LoadArgumentsTests()
        {

        }

        [Fact]
        public void LoadArgumentTest()
        {
            AppArguments appArgs = new AppArguments();
            appArgs.LoadArguments(new[] { @"--targetPath=C:\TargetPath", @"--targetDir=C:\TargetDir", @"--projectDir=C:\ProjectDir" });
            ReturnCode retCode;
            Assert.True(appArgs.ValidateArguments(out retCode));
            Assert.Equal(ReturnCode.EXIT_SUCCESS, retCode);
            Assert.Equal(@"C:\TargetPath", appArgs.TargetPath);
            Assert.Equal(@"C:\TargetDir", appArgs.TargetDir);
            Assert.Equal(@"C:\ProjectDir", appArgs.ProjectDir);
        }

        [Fact]
        public void MissingArgumentsTest()
        {
            MissingArgs(new[] { @"--targetPath=C:\TargetPath", @"--targetDir=C:\TargetDir" });
            MissingArgs(new[] { @"--targetPath=C:\TargetPath" });
            MissingArgs(new[] { @"--targetPath=" });
            MissingArgs(new string[0]);
        }

        private static void MissingArgs(string[] args)
        {
            AppArguments appArgs = new AppArguments();
            appArgs.LoadArguments(args);
            ReturnCode retCode;
            Assert.False(appArgs.ValidateArguments(out retCode));
            Assert.Equal(ReturnCode.EXIT_ERROR, retCode);
        }

        [Fact]
        public void HelpTest()
        {
            AppArguments appArgs = new AppArguments();
            appArgs.LoadArguments(new[] { @"--h" });
            ReturnCode retCode;
            Assert.False(appArgs.ValidateArguments(out retCode));
            Assert.Equal(ReturnCode.EXIT_SUCCESS, retCode);
        }
    }
}