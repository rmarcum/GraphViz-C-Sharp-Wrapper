using FluentAssertions;

using GraphVizWrapper.Commands;
using GraphVizWrapper.Queries;

using Moq;

using System.Diagnostics;

using Xunit;

namespace GraphVizWrapper.Tests
{
    public class Tests
    {
        private readonly IGetStartProcessQuery _getStartProcessQuery;
        private readonly IGraphGeneration _wrapper;
        public Tests()
        {
            _getStartProcessQuery = new GetStartProcessQuery();

            var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            var registerLayoutPluginCommand = new RegisterLayoutPluginCommand(getProcessStartInfoQuery, _getStartProcessQuery);
            _wrapper = new GraphGeneration(
                _getStartProcessQuery,
                getProcessStartInfoQuery,
                registerLayoutPluginCommand);
        }

        [Fact]
        public void GenerateGraphReturnsByteArrayWithLengthGreaterThanZero()
        {
            // Arrange
            //var getProcessStartInfoQueryMock = new Mock<IGetProcessStartInfoQuery>();
            //var registerLayoutPluginCommandMock = new Mock<IRegisterLayoutPluginCommand>();

            //registerLayoutPluginCommandMock.Setup(m => m.Invoke(It.IsAny<string>(), Enums.RenderingEngine.Dot));
            //getProcessStartInfoQueryMock.Setup(m => m.Invoke(It.IsAny<IProcessStartInfoWrapper>())).Returns(
            //    new ProcessStartInfo
            //    {
            //        FileName = "cmd",
            //        RedirectStandardInput = true,
            //        RedirectStandardOutput = true,
            //        RedirectStandardError = true,
            //        UseShellExecute = false,
            //        CreateNoWindow = true
            //    });

            //var wrapper = new GraphGeneration(
            //    _getStartProcessQuery,
            //    getProcessStartInfoQueryMock.Object,
            //    registerLayoutPluginCommandMock.Object);
            _wrapper.RenderingEngine = Enums.RenderingEngine.Dot;
            _wrapper.GraphvizPath = "sfsdf";
            // Act
            byte[] output = _wrapper.GenerateGraph("digraph{a -> b; b -> c; c -> a;}", Enums.GraphReturnType.Png);
            // Assert
            output.Length.Should().BePositive();
        }

        [Fact]
        public void DoesNotCrashWithLargeInput()
        {
            // Arrange
            //var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            //var registerLayoutPluginCommand = new RegisterLayoutPluginCommand(getProcessStartInfoQuery, _getStartProcessQuery);

            //var wrapper = new GraphGeneration(
            //    _getStartProcessQuery,
            //    getProcessStartInfoQuery,
            //    registerLayoutPluginCommand);

            // Act
            var diagraph =
                "digraph G {subgraph cluster_T1 { label = \"WORK ORDER\"; S1 [label = \"ACCEPTED\"]; S2 [label = \"DENIED\"]; S5 [label = \"SCHEDULED\"]; S44 [label = \"COMPLETE\"]; S47 [label = \"DELIVERED\"]; S48 [label = \"CANCELLED\"]; } subgraph cluster_T2 { label = \"MILL/DIG CHECK OUT\"; S3 [label = \"PROGRAMMING\"]; S4 [label = \"CUTTER PATH\"]; } subgraph cluster_T24 { label = \"OFFLOAD INTERNAL\"; S83 [label = \"IN PROCESS\"]; S84 [label = \"FINISHED\"]; } subgraph cluster_T23 { label = \"OFFLOAD EXTERNAL\"; S45 [label = \"IN PROCESS\"]; S46 [label = \"FINISHED\"]; S82 [label = \"QUOTE PENDING\"]; } subgraph cluster_T1 { label = \"WORK ORDER\"; S49 [label = \"WAITING 5-AXIS\"]; S50 [label = \"WAITING DATA CHANGE\"]; } subgraph cluster_T8 { label = \"MILL 1\"; S15 [label = \"IN PROCESS\"]; S16 [label = \"FINISHED\"]; } subgraph cluster_T9 { label = \"MILL 2\"; S17 [label = \"IN PROCESS\"]; S18 [label = \"FINISHED\"]; } subgraph cluster_T10 { label = \"MILL 3\"; S20 [label = \"IN PROCESS\"]; S21 [label = \"FINISHED\"]; } subgraph cluster_T11 { label = \"MILL 4\"; S22 [label = \"IN PROCESS\"]; S23 [label = \"FINISHED\"]; } subgraph cluster_T12 { label = \"MILL 5\"; S24 [label = \"IN PROCESS\"]; S25 [label = \"FINISHED\"]; } subgraph cluster_T13 { label = \"MILL 6\"; S26 [label = \"IN PROCESS\"]; S27 [label = \"FINISHED\"]; } subgraph cluster_T14 { label = \"MILL 7\"; S28 [label = \"IN PROCESS\"]; S29 [label = \"FINISHED\"]; } subgraph cluster_T15 { label = \"MILL 8\"; S30 [label = \"IN PROCESS\"]; S31 [label = \"FINISHED\"]; } subgraph cluster_T16 { label = \"HAAS\"; S32 [label = \"IN PROCESS\"]; S33 [label = \"FINISHED\"]; } subgraph cluster_T17 { label = \"FADAL 1\"; S34 [label = \"IN PROCESS\"]; S35 [label = \"FINISHED\"]; } subgraph cluster_T18 { label = \"FADAL 2\"; S36 [label = \"IN PROCESS\"]; S37 [label = \"FINISHED\"]; } subgraph cluster_T19 { label = \"DUPLICATOR\"; S38 [label = \"IN PROCESS\"]; S39 [label = \"FINISHED\"]; } subgraph cluster_T20 { label = \"TWIN RED\"; S40 [label = \"IN PROCESS\"]; S41 [label = \"FINISHED\"]; } subgraph cluster_T21 { label = \"TWIN BLUE\"; S42 [label = \"IN PROCESS\"]; S43 [label = \"FINISHED\"]; } S1->S3;S47->S44;S3->S4;S4->S5;S83->S84;S84->S47;S45->S46;S46->S47;S82->S45;S15->S16;S16->S47;S17->S18;S18->S47;S20->S21;S21->S47;S22->S23;S23->S47;S24->S25;S25->S47;S26->S27;S27->S47;S28->S29;S29->S47;S30->S31;S31->S47;S32->S33;S33->S47;S34->S35;S35->S47;S36->S37;S37->S47;S38->S39;S39->S47;S40->S41;S41->S47;S42->S43;S43->S47;}";

            byte[] output = _wrapper.GenerateGraph(diagraph, Enums.GraphReturnType.Png);
            output.Length.Should().BeGreaterThan(100000);
        }

        [Fact]
        public void AllowsPlainTextOutputType()
        {
            // Arrange
            //var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            //var registerLayoutPluginCommand = new RegisterLayoutPluginCommand(getProcessStartInfoQuery, _getStartProcessQuery);

            //var wrapper = new GraphGeneration(
            //    _getStartProcessQuery,
            //    getProcessStartInfoQuery,
            //    registerLayoutPluginCommand);

            // Act
            byte[] output = _wrapper.GenerateGraph("digraph{a -> b; b -> c; c -> a;}", Enums.GraphReturnType.Plain);

            var graphPortion = System.Text.Encoding.Default.GetString(output).Split(new string[] { "\r\n" }, System.StringSplitOptions.None);

            graphPortion[0].Should().Be("graph 1 1.125 2.5");
        }

        [Fact]
        public void AllowsPlainExtTextOutputType()
        {
            // Arrange
            //var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            //var registerLayoutPluginCommand = new RegisterLayoutPluginCommand(getProcessStartInfoQuery, _getStartProcessQuery);

            //var wrapper = new GraphGeneration(
            //    _getStartProcessQuery,
            //    getProcessStartInfoQuery,
            //    registerLayoutPluginCommand);

            // Act
            byte[] output = _wrapper.GenerateGraph("digraph{a -> b; b -> c; c -> a;}", Enums.GraphReturnType.PlainExt);

            var graphPortion = System.Text.Encoding.Default.GetString(output).Split(new string[] { "\r\n" }, System.StringSplitOptions.None);

            graphPortion[0].Should().Be("graph 1 1.125 2.5");
        }
    }
}
