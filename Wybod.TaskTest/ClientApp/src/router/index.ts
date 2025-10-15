// import { createRouter, createWebHistory } from 'vue-router'
// import DefaultLayout from '@/layouts/DefaultLayout.vue'
// import TasksView from '@/views/TasksView.vue'

import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import TaskList from '../views/TaskList.vue';
import TaskDetail from '../views/TaskDetail.vue';
import TaskForm from '../views/TaskForm.vue';

const routes: Array<RouteRecordRaw> = [
  { path: '/', component: TaskList },
  { path: '/task/:id', component: TaskDetail },
  { path: '/create', component: TaskForm }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;



// const router = createRouter({
//   history: createWebHistory(),
//   routes: [
//     {
//       path: '/',
//       component: DefaultLayout,
//       children: [
//         {
//           path: '',
//           name: 'tasks',
//           component: TasksView
//         }
//       ]
//     }
//   ]
// })
//
// export default router
