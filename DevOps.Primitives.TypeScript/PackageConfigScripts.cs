﻿using Common.EnumStringValues;

namespace DevOps.Primitives.TypeScript
{
    public enum PackageConfigScripts : byte
    {
        [EnumStringValue(nameof(prepublish))]
        prepublish,
        [EnumStringValue(nameof(prepare))]
        prepare,
        [EnumStringValue(nameof(prepublishOnly))]
        prepublishOnly,
        [EnumStringValue(nameof(prepack))]
        prepack,
        [EnumStringValue(nameof(postpack))]
        postpack,
        [EnumStringValue(nameof(publish))]
        publish,
        [EnumStringValue(nameof(postpublish))]
        postpublish,
        [EnumStringValue(nameof(preinstall))]
        preinstall,
        [EnumStringValue(nameof(install))]
        install,
        [EnumStringValue(nameof(postinstall))]
        postinstall,
        [EnumStringValue(nameof(preuninstall))]
        preuninstall,
        [EnumStringValue(nameof(uninstall))]
        uninstall,
        [EnumStringValue(nameof(postuninstall))]
        postuninstall,
        [EnumStringValue(nameof(preversion))]
        preversion,
        [EnumStringValue(nameof(version))]
        version,
        [EnumStringValue(nameof(postversion))]
        postversion,
        [EnumStringValue(nameof(pretest))]
        pretest,
        [EnumStringValue(nameof(test))]
        test,
        [EnumStringValue(nameof(posttest))]
        posttest,
        [EnumStringValue(nameof(prestop))]
        prestop,
        [EnumStringValue(nameof(stop))]
        stop,
        [EnumStringValue(nameof(poststop))]
        poststop,
        [EnumStringValue(nameof(prerestart))]
        prerestart,
        [EnumStringValue(nameof(restart))]
        restart,
        [EnumStringValue(nameof(postrestart))]
        postrestart,
        [EnumStringValue(nameof(preshrinkwrap))]
        preshrinkwrap,
        [EnumStringValue(nameof(shrinkwrap))]
        shrinkwrap,
        [EnumStringValue(nameof(postshrinkwrap))]
        postshrinkwrap
    }
}
