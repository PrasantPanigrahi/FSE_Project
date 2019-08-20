export interface Project {
    id: number;
    name: string;
    startDate: string;
    endDate: string;
    priority: string;
    managerDisplayName: string;
    managerId: number;
    totalTasks?: number;
    totalCompletedTasks?: number;
    isSuspended?: boolean;
    isSuspendedText?: string;
}
