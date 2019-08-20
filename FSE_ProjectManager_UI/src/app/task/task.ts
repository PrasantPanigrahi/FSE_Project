export interface Task {
    id: number;
    name: string;
    startDate: string;
    endDate: string;
    priority: string;
    parentTaskName: string;
    parentTaskId?: number;
    ownerName: string;
    ownerId: number;
    projectName: string;
    projectId: number;
    statusId?: number;
    isCompleted?: string;
}
