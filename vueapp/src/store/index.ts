import {RootState, rootState} from './state'
import {Mutations, rootMutations} from './mutations'

import {rootActions} from './actions'

import { InjectionKey } from 'vue'
import { createStore, useStore as baseUseStore, Store } from 'vuex'

export const key: InjectionKey<Store<RootState>> = Symbol()

const store = createStore<RootState>({
  state : rootState,
  mutations: rootMutations,
  actions : rootActions,
})

export function useStore () {
  return baseUseStore(key)
}
export default store