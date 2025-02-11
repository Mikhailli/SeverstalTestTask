﻿namespace WpfOrderManagementSystem.Infrastructure;

internal class EditorPanelClosedEventArgs<TEditedModel>
{
    public EditorPanelResult ResultType { get; }

    public TEditedModel EditedModel { get; }

    public EditorPanelClosedEventArgs(EditorPanelResult resultType, TEditedModel editedModel)
    {
        ResultType = resultType;
        EditedModel = editedModel;
    }
}
