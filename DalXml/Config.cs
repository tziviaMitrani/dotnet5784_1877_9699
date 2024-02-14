using System.Reflection.Metadata;

namespace Dal;
/// <summary>
//Handles getting a running id
/// </summary>
/// <param name="s_data_config_xml">The name of the xml file</param>
/// <param name="NextTaskId">Gets the id of the next task</param>
/// <param name="NextDependencyId">Gets the id of the next Dependency task</param>

internal static class Config
{
    static readonly string s_data_config_xml = "data-config";
    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }

}

