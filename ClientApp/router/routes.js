import CounterExample from 'components/counter-example'
import FetchData from 'components/fetch-data'
import HomePage from 'components/home-page'
import CustomerList from 'components/customer-list'
import CustomerForm from 'components/customer-form'

export const routes = [
  { path: '/', component: HomePage},
  { path: '/counter', component: CounterExample},
  { path: '/fetch-data', component: FetchData},
  { path: '/customers/edit/:id', component: CustomerForm},
  { path: '/customers/new', component: CustomerForm},
  { path: '/customers', component: CustomerList, display: 'Customers', icon: 'list' }
]
